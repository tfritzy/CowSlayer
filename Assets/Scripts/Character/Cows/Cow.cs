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

    public enum CowState
    {
        Grazing,
        Attacking
    }

    protected override void UpdateLoop()
    {
        AIUpdate();
        base.UpdateLoop();
    }

    protected virtual void AIUpdate()
    {
        switch (this.CurrentState)
        {
            case CowState.Grazing:
                Graze();
                break;
            case CowState.Attacking:
                PrimaryAttack();
                break;
            default:
                Graze();
                break;
        }
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
            this.transform.localScale *= 2;
            this.MeleeAttackRange *= 1.5f;
            this.RangedAttackRange *= 1.5f;
        }
    }

    public override void PrimaryAttack()
    {
        if (Target == null)
        {
            if (this.CurrentState == CowState.Attacking)
            {
                this.Body.Animator.speed = 1;
                this.CurrentState = CowState.Grazing;
                this.CurrentAnimation = AnimationState.Idle;
                this.targetPosition = FindNewGrazePosition();
            }

            return;
        }

        if ((Target.transform.position - this.transform.position).magnitude > GetAttackRange(this.PrimarySkill))
        {
            this.CurrentAnimation = AnimationState.Walking;
            MoveTowards(Target.transform.position);
        }
        else
        {
            targetPosition = this.transform.position;
            this.CurrentAnimation = AnimationState.Attacking;
            LookTowards(Target.transform.position);
            base.PrimaryAttack();
        }
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
        this.PrimarySkill = new Whack(this);
        base.Initialize();
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

        Vector3 diffVector = targetPosition - this.transform.position;
        if (diffVector.magnitude > .1f)
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

        if (Target != null)
        {
            this.CurrentState = CowState.Attacking;
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

        base.OnDeath();
    }

    public void PromoteToZoneGuardian()
    {
        this.IsZoneGuardian = true;
    }
}