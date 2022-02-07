using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cow : Character
{
    public CowState CurrentState;
    private Player player;
    public bool IsZoneGuardian;
    public int Zone;
    public int XPReward;
    public abstract CowType CowType { get; }
    public DropTable DropTable;

    // TODO: Choose stats based on cow's power.
    protected override int BaseMaxHealth => 5 + Level * 2;
    protected override int BaseDamage => 2 + Level;
    protected override int BaseMagicAffinity => 1;
    protected override float BaseAttackSpeedPercent => 1;
    protected override float BaseMovementSpeed => 2;
    protected override int BaseMaxMana => 10;
    protected override int BaseArmor => 3;


    private Vector3? _targetPosition;
    public Vector3? TargetPosition
    {
        get
        {
            return _targetPosition;
        }
        set
        {
            _targetPosition = value;
        }
    }


    protected float StareDistance => this.PrimarySkill.Range + 3f;
    private const float WALK_RANGE = 6 * 6;
    private const float BACK_KICK_RANGE = 1.25f;
    Vector3 vecToPlayer;

    private const string ANIMATION_STATE_PARAM = "Animation_State";
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
            this.Body.Animator.SetInteger(ANIMATION_STATE_PARAM, (int)_animationState);
        }
    }


    private float manaRegenPerMinute = 1;
    public override float ManaRegenPerMinute => manaRegenPerMinute;

    public override void Initialize()
    {
        base.Initialize();
        this.player = Constants.Persistant.PlayerScript;
        this.Target = this.player;
        this.Allegiance = Allegiance.Cows;
        this.Enemies = new HashSet<Allegiance>() { Allegiance.Player };
        this.Name += Guid.NewGuid().ToString("N");
        this.name = this.Name;
        this.Zone = int.Parse(transform.parent.name.Split('_')[1]);
        this.Level = this.Zone + 1;
    }

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
        TurningTowardsTarget,
        Flinching,
    }

    protected override void UpdateLoop()
    {
        base.UpdateLoop();


        switch (CurrentState)
        {
            case (CowState.Grazing):
                Graze();
                return;
            // case (CowState.KickingBackwards):
            //     KickBackwards();
            //     return;
            case (CowState.EatingGrass):
                EatGrass();
                return;
            case (CowState.WalkingTowardsPlayer):
                WalkTowardsPlayer();
                return;
            // case (CowState.WindingUpCharge):
            //     WindUpCharge();
            //     return;
            // case (CowState.Charging):
            //     Charge();
            //     return;
            // case (CowState.SkiddingToStop):
            //     SkidToStop();
            //     return;
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
            case (CowState.TurningTowardsTarget):
                TurnTowardsTarget();
                return;
            case (CowState.Flinching):
                Flinch();
                return;
        }
    }

    private const float FIND_GRAZE_POS_SIZE = 15;
    private void Graze()
    {
        vecToPlayer = player.Position - this.Position;
        float distToPlayer = vecToPlayer.sqrMagnitude;

        if (ShouldFlee() && distToPlayer < StareDistance)
        {
            this.CurrentState = CowState.Fleeing;
            return;
        }

        if (distToPlayer < BACK_KICK_RANGE)
        {
            Vector3 backwards = this.Position - this.Body.Forward;

            if (Vector3.Angle(vecToPlayer, backwards) < 20)
            {
                this.CurrentState = CowState.KickingBackwards;
                this.TargetPosition = Vector3.zero;
                return;
            }
        }

        if (distToPlayer < WALK_RANGE)
        {
            this.CurrentState = CowState.WalkingTowardsPlayer;
            this.TargetPosition = null;
            return;
        }
        // else if (distToPlayer < CHARGE_DISTANCE)
        // {
        //     this.CurrentState = CowState.WindingUpCharge;
        //     this.TargetPosition = null;
        //     return;
        // }
        else if (distToPlayer < StareDistance)
        {
            this.CurrentState = CowState.StaringAtPlayer;
            this.TargetPosition = null;
            return;
        }

        if (TargetPosition == null)
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

            this.TargetPosition = new Vector3(
                UnityEngine.Random.Range(
                    chooseGrazePosMid.x - FIND_GRAZE_POS_SIZE / 2,
                    chooseGrazePosMid.x + FIND_GRAZE_POS_SIZE / 2),
                0,
                UnityEngine.Random.Range(
                    chooseGrazePosMid.z - FIND_GRAZE_POS_SIZE / 2,
                    chooseGrazePosMid.z + FIND_GRAZE_POS_SIZE / 2)
            );
        }

        float distToGrazePos = (this.TargetPosition.Value - this.Position).sqrMagnitude;
        if (distToGrazePos < .1f)
        {
            this.CurrentState = CowState.EatingGrass;
            this.Freeze();
            this.TargetPosition = Vector3.zero;
        }
        else
        {
            this.SetVelocityTowardsPoint(TargetPosition.Value, this.MovementSpeed);
            this.AnimationState = CowAnimationState.Walking;
        }
    }

    private void KickBackwards()
    {
        this.AnimationState = CowAnimationState.KickingBackwards;
        this.Freeze();
    }

    public void BackwardsKicKAnimTrigger()
    {
        vecToPlayer = this.player.Position - this.Position;
        Vector3 backwards = this.Position - this.Body.Forward;

        if (vecToPlayer.sqrMagnitude < BACK_KICK_RANGE && Vector3.Angle(vecToPlayer, backwards) < 15)
        {
            this.player.TakeDamage(this.Damage * 3, this);
        }

        this.CurrentState = CowState.Grazing;
    }

    public void FinishBackKickAnimTrigger()
    {
        this.AnimationState = CowAnimationState.Idle;
        this.CurrentState = CowState.Grazing;
    }

    private void EatGrass()
    {
        this.AnimationState = CowAnimationState.EatingGrass;
        this.Freeze();
    }

    public void FinishedEatingGrassAnimTrigger()
    {
        this.CurrentState = CowState.Grazing;
    }

    private float chargeStartTime;
    private void WindUpCharge()
    {
        this.AnimationState = CowAnimationState.WindingUpCharge;
        this.Freeze();
    }

    public void FinishedWindingUpChargeAnimTrigger()
    {
        this.AnimationState = CowAnimationState.Charging;
        this.CurrentState = CowState.Charging;
        this.chargeStartTime = Time.time;
    }

    private const float STOP_CHARGE_DISTANCE = 3 * 3;
    private const float MAX_CHARGE_TIME_S = 5f;
    private void Charge()
    {
        vecToPlayer = this.player.Position - this.Position;

        if (vecToPlayer.sqrMagnitude < STOP_CHARGE_DISTANCE || Time.time > chargeStartTime + MAX_CHARGE_TIME_S)
        {
            this.CurrentState = CowState.SkiddingToStop;
            this.Freeze();
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
        this.Freeze();

        if (distToPlayer < this.PrimarySkill.Range * .8f)
        {
            this.CurrentState = CowState.Fighting;
            return;
        }
        else if (distToPlayer < WALK_RANGE)
        {
            this.CurrentState = CowState.WalkingTowardsPlayer;
            return;
        }
        else
        {
            this.CurrentState = CowState.Grazing;
            return;
        }
    }


    private void Fight()
    {
        vecToPlayer = this.player.Position - this.Position;

        this.Freeze();

        if (this.ShouldFlee())
        {
            this.CurrentState = CowState.Fleeing;
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
            this.CurrentState = CowState.PerformingAttack;
            return;
        }
        else if (distToPlayer > this.PrimarySkill.Range * .9f)
        {
            this.CurrentState = CowState.WalkingTowardsPlayer;
        }
    }

    public override void AttackAnimTrigger()
    {
        base.AttackAnimTrigger();
        this.CurrentState = CowState.TurningTowardsTarget;
    }

    private bool ShouldFlee()
    {
        return this.Health < this.MaxHealth * .1f;
    }

    private void Flee()
    {
        Vector3 vecAwayFromPlayer = this.Position - this.player.Position;

        if (vecAwayFromPlayer.sqrMagnitude > StareDistance)
        {
            this.CurrentState = CowState.Grazing;
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
            this.CurrentState = CowState.Grazing;
            return;
        }
        else if (distToPlayer < this.PrimarySkill.Range * .8f)
        {
            this.CurrentState = CowState.Fighting;
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

        this.Freeze();

        Quaternion lookRotation = Quaternion.LookRotation(vecToPlayer, Vector3.up);
        if (Quaternion.Angle(this.Body.Rotation, lookRotation) > 5)
        {
            this.CurrentState = CowState.TurningTowardsTarget;
            return;
        }
        else
        {
            this.AnimationState = CowAnimationState.Idle;
        }

        if (distToPlayer < WALK_RANGE)
        {
            this.CurrentState = CowState.WalkingTowardsPlayer;
            return;
        }
        // else if (distToPlayer < CHARGE_DISTANCE)
        // {
        //     this.CurrentState = CowState.WindingUpCharge;
        //     return;
        // }
        else if (distToPlayer > StareDistance)
        {
            this.CurrentState = CowState.Grazing;
            return;
        }
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
        this.transform.localScale *= 2;
        this.gameObject.name = "ZoneGuardian " + Zone;
    }

    protected override void SetAttackAnimation(Skill skill)
    {
        this.AnimationState = CowAnimationState.Attacking;
    }

    public void TurnTowardsTarget()
    {
        float remainingAngle = float.MaxValue;
        remainingAngle = RotateTowardsPoint(this.player.Position);
        this.AnimationState = CowAnimationState.Turning;

        if (remainingAngle < 1)
        {
            vecToPlayer = player.Position - this.Position;
            float distToPlayer = vecToPlayer.sqrMagnitude;

            if (distToPlayer < WALK_RANGE)
            {
                this.CurrentState = CowState.WalkingTowardsPlayer;
                this.TargetPosition = null;
                return;
            }
            else if (distToPlayer < StareDistance)
            {
                this.CurrentState = CowState.StaringAtPlayer;
                this.TargetPosition = null;
                return;
            }
            else
            {
                this.CurrentState = CowState.Grazing;
                this.TargetPosition = null;
                return;
            }
        }
    }

    public override void TakeDamage(int amount, Character attacker)
    {
        base.TakeDamage(amount, attacker);
        this.CurrentState = CowState.Flinching;
    }

    public void Flinch()
    {
        this.AnimationState = CowAnimationState.Flinching;
        this.Freeze();
    }

    public void EndFlinchAnimTrigger()
    {
        this.CurrentState = CowState.Grazing;
    }
}