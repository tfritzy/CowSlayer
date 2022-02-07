using System.Collections.Generic;

public abstract class Staff : Weapon
{
    protected Staff(int level) : base(level)
    {
    }

    public override SkillType DefaultAttack => SkillType.Whack;
    public override PlayerAnimationState IdleAnimation => PlayerAnimationState.StaffIdle;
    public override PlayerAnimationState AttackAnimation => PlayerAnimationState.OneHandWeaponAttack;
    public override PlayerAnimationState WalkAnimation => PlayerAnimationState.StaffWalk;
    public override PlayerAnimationState RunAnimation => PlayerAnimationState.StaffRun;

    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            new FlatDamageStatModifier(this.Id, this.BasePower * .2f),
            new AttackSpeedStatModifier(this.Id, this.BasePower * -.2f),
            new MagicAffinityStatModifier(this.Id, this.BasePower * 1f),
        };
    }
}