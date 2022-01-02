using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sword : Weapon
{
    public override SkillType DefaultAttack => SkillType.SwordSwing;
    public override PlayerAnimationState IdleAnimation => PlayerAnimationState.IdleOneHandedWeapon;
    public override PlayerAnimationState AttackAnimation => PlayerAnimationState.OneHandWeaponAttack;
    public override PlayerAnimationState WalkAnimation => PlayerAnimationState.OneHandWeaponWalk;
    public override PlayerAnimationState RunAnimation => PlayerAnimationState.OneHandRun;
}

