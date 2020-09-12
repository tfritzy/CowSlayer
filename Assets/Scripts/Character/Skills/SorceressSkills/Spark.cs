using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MeleeSkill
{
    public override string Name => "Spark";
    public override float Cooldown => 1f;
    public override bool CanAttackWhileMoving => false;
    public override float DamagePercentIncrease => 1f;
    protected override string AttackPrefabName => "Sparks";
}
