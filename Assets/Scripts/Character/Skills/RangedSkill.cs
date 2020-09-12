using UnityEngine;
using System.Collections;

public abstract class RangedSkill : Skill
{
    protected abstract float MovementSpeed { get; }

    public override void Attack(Character attacker, AttackTargetingDetails attackTargetingDetails)
    {
        GameObject projectile = GameObject.Instantiate(AttackPrefab, attacker.transform.position, new Quaternion(), null);
        Vector3 flyDirection = attackTargetingDetails.TravelDirection;
        projectile.GetComponent<Rigidbody>().velocity = flyDirection.normalized * MovementSpeed;
        projectile.GetComponent<Projectile>().Initialize(DealDamage, attacker);
        base.Attack(attacker, attackTargetingDetails);
    }
}
