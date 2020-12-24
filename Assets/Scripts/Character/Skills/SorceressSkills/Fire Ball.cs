using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : RangedSkill
{
    public override string Name => "Fire Ball";
    public override float Cooldown => 2f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 20;
    protected override float MovementSpeed => 14f;
    protected override float ExplosionRadius => 1f;
}
