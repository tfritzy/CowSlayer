using UnityEngine;

public abstract class RangedSkill : Skill
{
    protected abstract float MovementSpeed { get; }
    protected virtual Vector3 ProjectileStartPositionOffset => Vector3.zero;

    public override bool Activate(Character attacker, AttackTargetingDetails attackTargetingDetails)
    {
        if (base.Activate(attacker, attackTargetingDetails) == false)
        {
            return false;
        }

        CreatePrefab(attackTargetingDetails);

        return true;
    }

    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails)
    {
        GameObject projectile = GameObject.Instantiate(Prefab, attackTargetingDetails.Attacker.transform.position + ProjectileStartPositionOffset, new Quaternion(), null);
        DirectProjectile(projectile, attackTargetingDetails, MovementSpeed);
    }
}
