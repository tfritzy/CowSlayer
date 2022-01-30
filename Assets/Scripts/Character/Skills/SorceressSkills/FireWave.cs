using UnityEngine;

public class FireWave : RangedSkill
{
    public override string Name => "Fire Wave";
    public override float Cooldown => 5f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 20;
    public override SkillType Type => SkillType.FireWave;
    public override float DamageModifier => 1f;
    public float WaveDistance => 5f + Level * 0.25f;
    public float Width => 1f + Level * 0.1f;
    public float FireDuration => 1f + Level * 0.25f;
    protected override float ProjectileSpeed => 0.5f;
    private const float DistBetweenFires = 0.4f;
    public override float Range => WaveDistance;

    public FireWave(Character bearer) : base(bearer)
    {
    }


    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails)
    {
        GameObject fireWave = GameObject.Instantiate(Prefab, attackTargetingDetails.Attacker.transform.position, new Quaternion());

        Vector3 position = attackTargetingDetails.Attacker.transform.position;
        position.y = Constants.WorldProperties.GroundLevel;
        Vector3 direction = attackTargetingDetails.Target.transform.position - attackTargetingDetails.Attacker.transform.position;
        direction = direction.normalized * DistBetweenFires;

        for (int i = 0; i < WaveDistance / DistBetweenFires; i++)
        {
            position += direction;
            GameObject fire = GameObject.Instantiate(
                Constants.Prefabs.GroundFire,
                position,
                new Quaternion(),
                fireWave.transform);

            fire.GetComponent<GroundFire>()
                .Setup(
                    CalculateDamage(attackTargetingDetails.Attacker),
                    FireDuration,
                    attackTargetingDetails.Attacker,
                    i * .03f);
        }
    }
}