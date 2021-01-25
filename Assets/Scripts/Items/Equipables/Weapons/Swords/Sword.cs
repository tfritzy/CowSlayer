using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sword : Weapon
{
    public override SkillType DefaultAttack => SkillType.SwordSwing;
    public override AnimationState IdleAnimation => AnimationState.IdleOneHandedWeapon;
    public override AnimationState AttackAnimation => AnimationState.OneHandWeaponAttack;
    public override AnimationState WalkAnimation => AnimationState.NormalWalk;

}

