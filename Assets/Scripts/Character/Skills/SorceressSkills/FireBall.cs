using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : RangedSkill
{
    public int GroundFireCount => 1 + Level / 3;
    public int GroundFireDamage => 1 * Level;
    public float GroundFireDuration => 2f + Level * 0.25f;
    public override string Name => "Fire Ball";
    public override float Cooldown => 2f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 20;
    public override SkillType Type => SkillType.Fireball;
    public override bool IsPassive => false;
    public override float DamageModifier => 3f + 0.25f * Level;
    protected override float MovementSpeed => 14f;
    protected override float ExplosionRadius => 1f;
    protected override string IconFilePath => $"{Constants.FilePaths.Icons}/FireBall";

    protected override void CreateGroundEffects(Character attacker, Vector3 position)
    {
        List<GameObject> fires = SpawnObjectsInCircle(Constants.Prefabs.GroundFire, GroundFireCount, position, (float)GroundFireCount / 8f);
        foreach (GameObject fire in fires)
        {
            fire.GetComponent<GroundFire>().Setup(GroundFireDamage, GroundFireDuration, attacker);
        }
    }
}
