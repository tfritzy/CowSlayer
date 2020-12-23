using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : RangedSkill
{
    public override string Name => "Meteor";
    public override float Cooldown => 3f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 30;
    protected override float MovementSpeed => 25f;
    protected override Vector3 ProjectileStartPositionOffset => new Vector3(0, 30f, 0);
}
