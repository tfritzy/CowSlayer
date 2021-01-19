public abstract class Staff : Weapon
{
    public override SkillType DefaultAttack => SkillType.Whack;
    public override AnimationState IdleAnimation => AnimationState.IdleOneHandedWeapon;
    public override AnimationState AttackAnimation => AnimationState.OneHandWeaponAttack;
    public override AnimationState WalkAnimation => AnimationState.StaffWalk;
}