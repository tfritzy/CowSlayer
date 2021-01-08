using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public abstract class Cow : Character
{
    public CowState CurrentState;
    public float MovementSpeed;
    public DropTable DropTable;
    public abstract CowType CowType { get; }
    public bool IsZoneGuardian;
    public int Zone;
    public int XPReward;

    protected Rigidbody rb;
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

    protected override void SetVelocity()
    {
        Vector3 diffVector = this.targetPosition - this.transform.position;
        diffVector.y = 0;

        if (CurrentState == CowState.Attacking && diffVector.magnitude < GetAttackRange(PrimarySkill))
        {
            return;
        }

        if (diffVector.magnitude < .1f)
        {
            this.rb.velocity = Vector3.zero;
            this.targetPosition = this.transform.position;
            this.CurrentAnimation = AnimationState.Idle;
            return;
        }
        else
        {
            this.CurrentAnimation = AnimationState.Walking;
        }

        this.rb.velocity = diffVector.normalized * MovementSpeed;
        SetRotationWithVelocity();
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
            this.targetPosition = Target.transform.position;
            this.CurrentAnimation = AnimationState.Walking;
        }
        else
        {
            targetPosition = this.transform.position;
            this.CurrentAnimation = AnimationState.Attacking;
            base.PrimaryAttack();
        }
    }

    public override void Initialize()
    {
        this.Allegiance = Allegiance.Cows;
        this.Enemies = new HashSet<Allegiance>() { Allegiance.Player };
        this.rb = this.GetComponent<Rigidbody>();
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
    private const float timeBetweenGrazePositionChanges = 5f;
    protected void Graze()
    {
        if (Time.time > lastGrazePositionTimeChange + timeBetweenGrazePositionChanges)
        {
            this.targetPosition = FindNewGrazePosition();
            lastGrazePositionTimeChange = Time.time;
        }

        CheckForTarget();
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