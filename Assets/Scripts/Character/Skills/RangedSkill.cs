using UnityEngine;

public abstract class RangedSkill : Skill
{
    protected abstract float ProjectileSpeed { get; }
    protected virtual Vector3 ProjectileStartPositionOffset => Vector3.zero;

    public RangedSkill(Character bearer) : base(bearer)
    {
    }

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
        Vector3 startPosition;
        if (attackTargetingDetails.Attacker.WornItems.Weapon != null)
        {
            startPosition = attackTargetingDetails.Attacker.WornItems.Weapon.ProjectileStartPosition;
        }
        else
        {
            startPosition = attackTargetingDetails.Attacker.Body.MainHand.transform.position;
        }

        startPosition += ProjectileStartPositionOffset;

        GameObject projectile = GameObject.Instantiate(
            Prefab,
            startPosition,
            new Quaternion(),
            null);
        DirectProjectile(projectile, attackTargetingDetails, ProjectileSpeed);
    }
}
