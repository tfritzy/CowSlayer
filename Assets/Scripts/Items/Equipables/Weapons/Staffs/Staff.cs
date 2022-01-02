public abstract class Staff : Weapon
{
    public override SkillType DefaultAttack => SkillType.Whack;
    public override PlayerAnimationState IdleAnimation => PlayerAnimationState.StaffIdle;
    public override PlayerAnimationState AttackAnimation => PlayerAnimationState.OneHandWeaponAttack;
    public override PlayerAnimationState WalkAnimation => PlayerAnimationState.StaffWalk;
    public override PlayerAnimationState RunAnimation => PlayerAnimationState.StaffRun;
}