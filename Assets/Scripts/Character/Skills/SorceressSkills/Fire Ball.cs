using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : RangedSkill
{
    public override string Name => "Fire Ball";
    public override float Cooldown => 2f;
    public override bool CanAttackWhileMoving => false;
    public override float DamagePercentIncrease => 3f;
    protected override float MovementSpeed => 14f;
    protected override string AttackPrefabName => "Fire Ball";
}
