using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cow : Character
{
    public enum CowState
    {
        Grazing,
        EatingGrass,
        StaringAtPlayer,
        KickingBackwards,
        WindingUpCharge,
        Charging,
        SkiddingToStop,
        Fighting,
        PerformingAttack,
        Fleeing,
        WalkingTowardsPlayer,
    }

    [SerializeField]
    private CowState _currentState;
    public CowState CurrentState => _currentState;
    private Player player;
    public bool IsZoneGuardian;
    public int Zone;
    public int XPReward;
    public abstract CowType CowType { get; }
    public DropTable DropTable;

    protected virtual float Scale => 1f;


    private const float STARE_DISTANCE = 15 * 15;
    private const float CHARGE_DISTANCE = 9 * 9;
    private const float WALK_RANGE = 6 * 6;
    private float BACK_KICK_RANGE;
    Vector3 vecToPlayer;

    private CowAnimationState _animationState;
    public CowAnimationState AnimationState
    {
        get
        {
            return _animationState;
        }
        set
        {
            _animationState = value;
            this.Body.Animator.SetInteger("Animation_State", (int)_animationState);
        }
    }

    public override float ManaRegenPerMinute => throw new NotImplementedException();

    public override void Initialize()
    {
        base.Initialize();
        this.player = Constants.Persistant.PlayerScript;
        this.BACK_KICK_RANGE = (1.25f * Scale) * (1.25f * Scale);
        this.Target = this.player;
        this.Allegiance = Allegiance.Cows;
        this.Enemies = new HashSet<Allegiance>() { Allegiance.Player };
        this.Name += Guid.NewGuid().ToString("N");
        this.name = this.Name;
        this.Zone = int.Parse(transform.parent.name.Split('_')[1]);
        this.Level = this.Zone + 1;
        this.PrimarySkill = new Whack();
    }

    protected override void UpdateLoop()
    {
        base.UpdateLoop();


        switch (CurrentState)
        {
            case (CowState.Grazing):
                Graze();
                return;
            case (CowState.KickingBackwards):
                KickBackwards();
                return;
            case (CowState.EatingGrass):
                EatGrass();
                return;
            case (CowState.WalkingTowardsPlayer):
                WalkTowardsPlayer();
                return;
            case (CowState.WindingUpCharge):
                WindUpCharge();
                return;
            case (CowState.Charging):
                Charge();
                return;
            case (CowState.SkiddingToStop):
                SkidToStop();
                return;
            case (CowState.Fighting):
                Fight();
                return;
            case (CowState.PerformingAttack):
                // no-op, just wait for anim to finish.
                return;
            case (CowState.Fleeing):
                Flee();
                return;
            case (CowState.StaringAtPlayer):
                StareAtPlayer();
                return;
        }
    }

    private Vector3 targetGrazePos;
    private const float FIND_GRAZE_POS_SIZE = 15;
    private void Graze()
    {
        vecToPlayer = player.Position - this.Position;
        float distToPlayer = vecToPlayer.sqrMagnitude;

        if (ShouldFlee() && distToPlayer < STARE_DISTANCE)
        {
            this._currentState = CowState.Fleeing;
            return;
        }

        if (distToPlayer < BACK_KICK_RANGE)
        {
            Vector3 backwards = this.Position - this.Body.Forward;

            if (Vector3.Angle(vecToPlayer, backwards) < 20)
            {
                this._currentState = CowState.KickingBackwards;
                this.targetGrazePos = Vector3.zero;
                return;
            }
        }

        if (distToPlayer < WALK_RANGE)
        {
            this._currentState = CowState.WalkingTowardsPlayer;
            this.targetGrazePos = Vector3.zero;
            return;
        }
        else if (distToPlayer < CHARGE_DISTANCE)
        {
            this._currentState = CowState.WindingUpCharge;
            this.targetGrazePos = Vector3.zero;
            return;
        }
        else if (distToPlayer < STARE_DISTANCE)
        {
            this._currentState = CowState.StaringAtPlayer;
            this.targetGrazePos = Vector3.zero;
            return;
        }

        if (targetGrazePos == Vector3.zero)
        {
            Vector3 chooseGrazePosMid = this.Position;

            float minX = -Constants.WorldProperties.HalfWidth + FIND_GRAZE_POS_SIZE / 2;
            if (chooseGrazePosMid.x < minX)
            {
                chooseGrazePosMid.x = minX;
            }

            float maxX = Constants.WorldProperties.HalfWidth - FIND_GRAZE_POS_SIZE / 2;
            if (chooseGrazePosMid.x > maxX)
            {
                chooseGrazePosMid.x = maxX;
            }

            // TODO: Limit z pos to be in zone.

            this.targetGrazePos = new Vector3(
                UnityEngine.Random.Range(
                    chooseGrazePosMid.x - FIND_GRAZE_POS_SIZE / 2,
                    chooseGrazePosMid.x + FIND_GRAZE_POS_SIZE / 2),
                0,
                UnityEngine.Random.Range(
                    chooseGrazePosMid.z - FIND_GRAZE_POS_SIZE / 2,
                    chooseGrazePosMid.z + FIND_GRAZE_POS_SIZE / 2)
            );
        }

        float distToGrazePos = (this.targetGrazePos - this.Position).sqrMagnitude;
        if (distToGrazePos < .1f)
        {
            this._currentState = CowState.EatingGrass;
            this.rb.velocity = Vector3.zero;
            this.targetGrazePos = Vector3.zero;
        }
        else
        {
            this.SetVelocityTowardsPoint(targetGrazePos, this.MovementSpeed);
            this.AnimationState = CowAnimationState.Walking;
        }
    }

    private void KickBackwards()
    {
        this.AnimationState = CowAnimationState.KickingBackwards;
        this.rb.velocity = Vector3.zero;
    }

    public void BackwardsKicKAnimTrigger()
    {
        vecToPlayer = this.player.Position - this.Position;
        Vector3 backwards = this.Position - this.Body.Forward;

        if (vecToPlayer.sqrMagnitude < BACK_KICK_RANGE && Vector3.Angle(vecToPlayer, backwards) < 15)
        {
            this.player.TakeDamage(this.Damage * 3, this);
        }

        this._currentState = CowState.Grazing;
    }

    public void FinishBackKickAnimTrigger()
    {
        this.AnimationState = CowAnimationState.Idle;
        this._currentState = CowState.Grazing;
    }

    private void EatGrass()
    {
        this.AnimationState = CowAnimationState.EatingGrass;
        this.rb.velocity = Vector3.zero;
    }

    public void FinishedEatingGrassAnimTrigger()
    {
        this._currentState = CowState.Grazing;
    }

    private float chargeStartTime;
    private void WindUpCharge()
    {
        this.AnimationState = CowAnimationState.WindingUpCharge;
        this.rb.velocity = Vector3.zero;
    }

    public void FinishedWindingUpChargeAnimTrigger()
    {
        this.AnimationState = CowAnimationState.Charging;
        this._currentState = CowState.Charging;
        this.chargeStartTime = Time.time;
    }

    private const float STOP_CHARGE_DISTANCE = 3 * 3;
    private const float MAX_CHARGE_TIME_S = 5f;
    private void Charge()
    {
        vecToPlayer = this.player.Position - this.Position;

        if (vecToPlayer.sqrMagnitude < STOP_CHARGE_DISTANCE || Time.time > chargeStartTime + MAX_CHARGE_TIME_S)
        {
            this._currentState = CowState.SkiddingToStop;
            this.rb.velocity = Vector3.zero;
            return;
        }
        else
        {
            this.SetVelocityTowardsPoint(this.player.Position, this.MovementSpeed * 2);
            this.AnimationState = CowAnimationState.Charging;
        }
    }

    private void SkidToStop()
    {
        this.AnimationState = CowAnimationState.SkiddingToStop;

        // TODO: Slow down slowly.
    }

    public void FinishedSkiddingToStopAnimTrigger()
    {
        vecToPlayer = this.player.Position - this.Position;
        float distToPlayer = this.DistanceToCharacter(this.player);
        this.rb.velocity = Vector3.zero;

        if (distToPlayer < GetAttackRange(this.PrimarySkill) * .8f)
        {
            this._currentState = CowState.Fighting;
            return;
        }
        else if (distToPlayer < WALK_RANGE)
        {
            this._currentState = CowState.WalkingTowardsPlayer;
            return;
        }
        else
        {
            this._currentState = CowState.Grazing;
            return;
        }
    }


    private void Fight()
    {
        vecToPlayer = this.player.Position - this.Position;

        this.rb.velocity = Vector3.zero;

        if (this.ShouldFlee())
        {
            this._currentState = CowState.Fleeing;
            return;
        }

        float distToPlayer = this.DistanceToCharacter(this.player);
        this.AnimationState = CowAnimationState.Idle;

        Quaternion lookRotation = Quaternion.LookRotation(vecToPlayer);
        if (Quaternion.Angle(this.Body.Rotation, lookRotation) > 5)
        {
            this.RotateTowardsPoint(this.player.Position);
            this.AnimationState = CowAnimationState.Turning;
            return;
        }
        else
        {
            this.AnimationState = CowAnimationState.Idle;
        }

        if (this.CanPerformAttack(this.PrimarySkill))
        {
            this.PerformAttack(this.PrimarySkill);
            this._currentState = CowState.PerformingAttack;
            return;
        }
        else if (distToPlayer > GetAttackRange(this.PrimarySkill) * .9f)
        {
            this._currentState = CowState.WalkingTowardsPlayer;
        }
    }

    public override void AttackAnimTrigger()
    {
        base.AttackAnimTrigger();
        this._currentState = CowState.Fighting;
    }

    private bool ShouldFlee()
    {
        return this.Health < this.MaxHealth * .1f;
    }

    private void Flee()
    {
        Vector3 vecAwayFromPlayer = this.Position - this.player.Position;

        if (vecAwayFromPlayer.sqrMagnitude > STARE_DISTANCE)
        {
            this._currentState = CowState.Grazing;
            return;
        }
        else
        {
            this.SetVelocityTowardsPoint(this.Position + vecAwayFromPlayer.normalized, this.MovementSpeed / 3);
            this.AnimationState = CowAnimationState.Limping;
            return;
        }
    }

    private void WalkTowardsPlayer()
    {
        vecToPlayer = this.player.Position - this.Position;
        float distToPlayer = this.DistanceToCharacter(this.player);

        if (distToPlayer > WALK_RANGE)
        {
            this._currentState = CowState.Grazing;
            return;
        }
        else if (distToPlayer < GetAttackRange(this.PrimarySkill) * .8f)
        {
            this._currentState = CowState.Fighting;
            return;
        }
        else
        {
            this.SetVelocityTowardsPoint(this.player.Position, this.MovementSpeed);
            this.AnimationState = CowAnimationState.Walking;
        }
    }

    private void StareAtPlayer()
    {
        vecToPlayer = this.player.Position - this.Position;
        float distToPlayer = vecToPlayer.sqrMagnitude;

        this.rb.velocity = Vector3.zero;

        Quaternion lookRotation = Quaternion.LookRotation(vecToPlayer);
        if (Quaternion.Angle(this.Body.Rotation, lookRotation) > 5)
        {
            this.RotateTowardsPoint(this.player.Position);
            this.AnimationState = CowAnimationState.Turning;
            return;
        }
        else
        {
            this.AnimationState = CowAnimationState.Idle;
        }

        if (distToPlayer < WALK_RANGE)
        {
            this._currentState = CowState.WalkingTowardsPlayer;
            return;
        }
        else if (distToPlayer < CHARGE_DISTANCE)
        {
            this._currentState = CowState.WindingUpCharge;
            return;
        }
        else if (distToPlayer > STARE_DISTANCE)
        {
            this._currentState = CowState.Grazing;
            return;
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

    protected override void OnDeath()
    {
        Constants.Persistant.PlayerScript.XP += XPReward;

        if (IsZoneGuardian)
        {
            GameState.Data.HighestZoneUnlocked = Math.Max(GameState.Data.HighestZoneUnlocked, Zone + 1);
            Constants.Persistant.ZoneManager.RefreshGates();
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
        this.SetInitialStats();
    }

    protected override void SetAttackAnimation(Skill skill)
    {
        this.AnimationState = CowAnimationState.Attacking;
    }
}