using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sword : Weapon
{
    protected Sword(int level) : base(level)
    {
    }

    public override SkillType DefaultAttack => SkillType.SwordSwing;
    public override PlayerAnimationState IdleAnimation => PlayerAnimationState.IdleOneHandedWeapon;
    public override PlayerAnimationState AttackAnimation => PlayerAnimationState.OneHandWeaponAttack;
    public override PlayerAnimationState WalkAnimation => PlayerAnimationState.OneHandWeaponWalk;
    public override PlayerAnimationState RunAnimation => PlayerAnimationState.OneHandRun;

    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            new FlatDamageStatModifier(this.Id, this.BasePower * .8f),
            new AttackSpeedStatModifier(this.Id, this.BasePower * .2f)
        };
    }
}

