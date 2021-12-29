public abstract class Staff : Weapon
{
    public override SkillType DefaultAttack => SkillType.Whack;
    public override AnimationState IdleAnimation => AnimationState.StaffIdle;
    public override AnimationState AttackAnimation => AnimationState.OneHandWeaponAttack;
    public override AnimationState WalkAnimation => AnimationState.StaffWalk;
    public override AnimationState RunAnimation => AnimationState.StaffRun;
}