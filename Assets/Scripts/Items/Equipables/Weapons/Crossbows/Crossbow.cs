using System.Collections.Generic;

public abstract class Crossbow : Weapon
{
    protected Crossbow(int level) : base(level)
    {
    }

    public override SkillType DefaultAttack => SkillType.CrossbowAttack;
    public override PlayerAnimationState IdleAnimation => PlayerAnimationState.IdleOneHandedWeapon;
    public override PlayerAnimationState AttackAnimation => PlayerAnimationState.CrossbowAttack;
    public override PlayerAnimationState WalkAnimation => PlayerAnimationState.OneHandWeaponWalk;
    public override PlayerAnimationState RunAnimation => PlayerAnimationState.OneHandRun;

    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            new FlatDamageStatModifier(this.Id, this.BasePower * 1.2f),
            new AttackSpeedStatModifier(this.Id, this.BasePower * -.2f),
        };
    }
}