using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MeleeSkill
{
    public override string Name => "Punch";
    public override float Cooldown => 2f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 0;
    public override SkillType Type => SkillType.Punch;
    public override float DamageModifier => 1f;
    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails) { }
}
