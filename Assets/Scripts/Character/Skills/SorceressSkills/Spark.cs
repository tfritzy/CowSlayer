using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MeleeSkill
{
    public override string Name => "Spark";
    public override float Cooldown => 1f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 3;
}
