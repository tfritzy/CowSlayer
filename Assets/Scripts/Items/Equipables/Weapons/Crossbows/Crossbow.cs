using System.Collections.Generic;

public abstract class Crossbow : Weapon
{
    public override SkillType DefaultAttack => SkillType.CrossbowAttack;
    public override PlayerAnimationState IdleAnimation => PlayerAnimationState.IdleOneHandedWeapon;
    public override PlayerAnimationState AttackAnimation => PlayerAnimationState.CrossbowAttack;
    public override PlayerAnimationState WalkAnimation => PlayerAnimationState.OneHandWeaponWalk;
    public override PlayerAnimationState RunAnimation => PlayerAnimationState.OneHandRun;
}