using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : RangedSkill
{
    public override string Name => "Meteor";
    public override float Cooldown => 3f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 30;
    protected override float MovementSpeed => 35f;
    protected override Vector3 ProjectileStartPositionOffset => new Vector3(-30f, 30f, 0);
    protected override float ExplosionRadius => 3f;

    public override bool IsCollisionTarget(Character attacker, GameObject collision)
    {
        return collision.CompareTag(Constants.Tags.Ground);
    }
}
