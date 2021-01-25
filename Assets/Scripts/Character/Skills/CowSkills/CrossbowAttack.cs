using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowAttack : RangedSkill
{
    public override string Name => "CrossbowAttack";
    public override float Cooldown => 3f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.CrossbowAttack;
    public override float DamageModifier => 2f;
    protected override float ProjectileSpeed => 20f;
    protected override Item Ammo => new Arrow();
}
