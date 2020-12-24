using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : RangedSkill
{
    public int GroundFireCount => 1;
    public int GroundFireDamage => 1;
    public float GroundFireDuration => 2f;
    public override string Name => "Fire Ball";
    public override float Cooldown => 2f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 20;

    protected override float MovementSpeed => 14f;
    protected override float ExplosionRadius => 1f;

    protected override void CreateGroundEffects(Character attacker, Vector3 position)
    {
        List<GameObject> fires = SpawnObjectsInCircle(Constants.Prefabs.GroundFire, GroundFireCount, position, (float)GroundFireCount / 8f);
        foreach (GameObject fire in fires)
        {
            fire.GetComponent<GroundFire>().Setup(GroundFireDamage, GroundFireDuration, attacker);
        }
    }
}
