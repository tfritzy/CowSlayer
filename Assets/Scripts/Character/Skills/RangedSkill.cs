using UnityEngine;

public abstract class RangedSkill : Skill
{
    protected abstract float MovementSpeed { get; }
    protected virtual Vector3 ProjectileStartPositionOffset => Vector3.zero;

    public override bool Attack(Character attacker, AttackTargetingDetails attackTargetingDetails)
    {
        if (base.Attack(attacker, attackTargetingDetails) == false)
        {
            return false;
        }

        GameObject projectile = GameObject.Instantiate(AttackPrefab, attacker.transform.position + ProjectileStartPositionOffset, new Quaternion(), null);
        Vector3 flyDirection = attackTargetingDetails.TravelDirection - ProjectileStartPositionOffset;
        projectile.GetComponent<Rigidbody>().velocity = flyDirection.normalized * MovementSpeed;
        projectile.GetComponent<Projectile>().Initialize(DealDamage, IsCollisionTarget, CreateGroundEffects, attacker);

        return true;
    }
}
