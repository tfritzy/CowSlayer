using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixWings : RangedSkill
{
    public override string Name => "PhoenixWings";
    public override float Cooldown => 3f;
    public override bool CanAttackWhileMoving => false;
    public override int ManaCost => 50;
    public override SkillType Type => SkillType.PhoenixWings;
    public override HashSet<SkillType> UnlockDependsOn => new HashSet<SkillType>()
    {
    };
    public override float DamageModifier => 3f + Level;
    public int NumberOfProjectiles => 6 + (Level / 3) * 2;
    protected override float ProjectileSpeed => 5f;
    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails)
    {
        Vector3 startPosition = attackTargetingDetails.Attacker.Body.Back.transform.position;
        Vector3 rightSide = attackTargetingDetails.Attacker.transform.up;
        Vector3 leftSide = attackTargetingDetails.Attacker.transform.up;
        rightSide = Quaternion.AngleAxis(25, attackTargetingDetails.Attacker.Body.Transform.forward) * rightSide;
        leftSide = Quaternion.AngleAxis(-25, attackTargetingDetails.Attacker.Body.Transform.forward) * leftSide;
        float angleBetweenProjectiles = 50 / ((float)NumberOfProjectiles / 2f);

        for (int i = 0; i < NumberOfProjectiles / 2; i++)
        {
            GameObject rightProjectile = GameObject.Instantiate(
                Prefab,
                startPosition,
                new Quaternion(),
                null);

            GameObject leftProjectile = GameObject.Instantiate(
                Prefab,
                startPosition,
                new Quaternion(),
                null);

            rightProjectile.GetComponent<Projectile>().Initialize(DealDamage, IsCollisionTarget, CreateGroundEffects, attackTargetingDetails.Attacker, target: attackTargetingDetails.Target);
            leftProjectile.GetComponent<Projectile>().Initialize(DealDamage, IsCollisionTarget, CreateGroundEffects, attackTargetingDetails.Attacker, target: attackTargetingDetails.Target);

            rightSide = Quaternion.AngleAxis(angleBetweenProjectiles, attackTargetingDetails.Attacker.transform.forward) * rightSide;
            leftSide = Quaternion.AngleAxis(-angleBetweenProjectiles, attackTargetingDetails.Attacker.transform.forward) * leftSide;

            rightProjectile.GetComponent<Rigidbody>().velocity = rightSide * ProjectileSpeed;
            leftProjectile.GetComponent<Rigidbody>().velocity = leftSide * ProjectileSpeed;
        }
    }
}
