using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public abstract class Cow : Character
{
    public CowState CurrentState;
    public DropTable DropTable;
    public abstract CowType CowType { get; }
    public bool IsZoneGuardian;
    public int Zone;
    public int XPReward;
    public bool IsInStateFreeze;
    public override int LineOfSightArcInDegrees => 120;

    public enum CowState
    {
        Grazing,
        Stalking,
        Attacking
    }

    protected override void SetInitialStats()
    {
        if (IsZoneGuardian)
        {
            this.MaxHealth *= 4;
            this.Health *= 4;
            this.Damage *= 2;
            this.MovementSpeed *= 1.3f;
            this.TargetFindRadius *= 2;
            this.Body.Transform.localScale *= 2;
            this.MeleeAttackRange *= 1.5f;
            this.RangedAttackRange *= 1.5f;
            this.gameObject.name = "ZoneGuardian " + Zone;
            Vector3 position = this.transform.position;
            position.y = Constants.WorldProperties.GroundLevel;
            this.transform.position = position;
        }

        // Place on ground.
        Vector3 newPosition = this.transform.position;
        newPosition.y = Constants.WorldProperties.GroundLevel +
                        this.Body.MeshRenderer.bounds.extents.y / 2 + .01f;
        this.transform.position = newPosition;
    }

    public override void Initialize()
    {
        this.Allegiance = Allegiance.Cows;
        this.Enemies = new HashSet<Allegiance>() { Allegiance.Player };
        this.Name += Guid.NewGuid().ToString("N");
        this.name = this.Name;
        this.targetPosition = FindNewGrazePosition();
        this.Zone = int.Parse(transform.parent.name.Split('_')[1]);
        this.Level = this.Zone + 1;
        this.PrimarySkill = new Whack();
        base.Initialize();
    }

    protected override void UpdateLoop()
    {
        AIUpdate();
        base.UpdateLoop();
    }

    public override void Attack()
    {
        PrimarySkill.Activate(this, BuildAttackTargetingDetails());
    }

    protected virtual void AIUpdate()
    {
        SetState();

        switch (this.CurrentState)
        {
            case CowState.Grazing:
                Graze();
                break;
            case CowState.Attacking:
                AttackLoop();
                break;
            case CowState.Stalking:
                Stalk();
                break;
            default:
                Graze();
                break;
        }
    }

    protected float AttackStartTime;
    public virtual void AttackLoop()
    {
        if (Target == null)
        {
            this.IsInStateFreeze = false;
            return;
        }

        float duration = this.Body.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        if (this.CurrentAnimation == AnimationState.Attacking &&
            Time.time < AttackStartTime + this.Body.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length)
        {
            this.IsInStateFreeze = true;
        }
        else if (this.PrimarySkill.IsOnCooldown())
        {
            LookTowards(Target.transform.position);
            this.IsInStateFreeze = false;
            this.CurrentAnimation = AnimationState.Idle;
            this.Body.Animator.speed = 1;
            FreezeRigidbody();
        }
        else
        {
            this.CurrentAnimation = AnimationState.Attacking;
            this.rb.constraints = RigidbodyConstraints.FreezeAll;
            this.Body.Animator.speed = AttackSpeedPercent;
            AttackStartTime = Time.time;
        }
    }

    private Vector3 targetPosition;
    private float lastGrazePositionTimeChange;
    private float timeBetweenGrazePositionChanges;
    protected void Graze()
    {
        if (Time.time > lastGrazePositionTimeChange + timeBetweenGrazePositionChanges)
        {
            timeBetweenGrazePositionChanges = Random.Range(2.5f, 7.5f);
            this.targetPosition = FindNewGrazePosition();
            lastGrazePositionTimeChange = Time.time;
        }

        if (Helpers.GetVectorBetween(targetPosition, this.transform.position).magnitude > .1f)
        {
            MoveTowards(targetPosition);
            this.CurrentAnimation = AnimationState.Walking;
        }
        else
        {
            this.rb.velocity = Vector3.zero;
            this.CurrentAnimation = AnimationState.Idle;
            this.targetPosition = this.transform.position;
        }
    }

    protected void Stalk()
    {
        if (Target == null)
        {
            return;
        }

        MoveTowards(Target.transform.position);
        this.CurrentAnimation = AnimationState.Walking;
    }

    private float lastStateCheckTime;
    private float timeBetweenStateCheck;
    protected virtual void SetState()
    {
        if (IsInStateFreeze)
        {
            return;
        }

        if (Time.time > lastStateCheckTime + timeBetweenStateCheck)
        {
            float distanceToTarget = float.MaxValue;
            if (Target != null)
            {
                distanceToTarget = GetDistBetweenColliders(Target.Body.Collider, Body.Collider);
            }

            if (distanceToTarget <= GetAttackRange(PrimarySkill))
            {
                this.CurrentState = CowState.Attacking;
            }
            else if (distanceToTarget <= TargetFindRadius)
            {
                this.CurrentState = CowState.Stalking;
            }
            else
            {
                this.CurrentState = CowState.Grazing;
            }

            SetRigidbodyConstraints();
            lastStateCheckTime = Time.time;
            timeBetweenStateCheck = Random.Range(.1f, .2f);
        }
    }

    private Vector3 FindNewGrazePosition()
    {
        return this.transform.position + new Vector3(Random.Range(-5f, 5f), this.transform.position.y, Random.Range(-5f, 5f));
    }

    protected override void OnDeath()
    {
        Constants.Persistant.PlayerScript.XP += XPReward;

        if (IsZoneGuardian)
        {
            GameState.Data.HighestZoneUnlocked = Math.Max(GameState.Data.HighestZoneUnlocked, Zone + 1);
            Constants.Persistant.ZoneManager.UnlockZones();
        }

        Drop drop = DropTable.RollDrop();
        if (drop != null)
        {
            GameObject dropContainer = GameObject.Instantiate<GameObject>(Constants.Prefabs.EmptyDrop, this.transform.position, new Quaternion(), null);
            dropContainer.GetComponent<DropContainer>().SetDrop(drop);
        }

        GameObject bloodSplat = Instantiate(Constants.Prefabs.Decals.BloodSplat);
        Helpers.PlaceDecalOnGround(this.transform.position, bloodSplat);
        bloodSplat.transform.localScale *= this.Body.Transform.localScale.x;
        bloodSplat.GetComponent<Decal>().Setup(8, 5);

        base.OnDeath();
    }

    public void PromoteToZoneGuardian()
    {
        this.IsZoneGuardian = true;
        this.SetInitialStats();
    }
}