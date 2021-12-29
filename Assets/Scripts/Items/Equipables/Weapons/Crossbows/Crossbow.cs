using System.Collections.Generic;

public abstract class Crossbow : Weapon
{
    public override SkillType DefaultAttack => SkillType.CrossbowAttack;
    public override AnimationState IdleAnimation => AnimationState.IdleOneHandedWeapon;
    public override AnimationState AttackAnimation => AnimationState.CrossbowAttack;
    public override AnimationState WalkAnimation => AnimationState.OneHandWeaponWalk;
    public override AnimationState RunAnimation => AnimationState.OneHandRun;
}