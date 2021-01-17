using UnityEngine;

public abstract class MeleeSkill : Skill
{
    public override bool Activate(Character attacker, AttackTargetingDetails attackTargetingDetails)
    {
        if (base.Activate(attacker, attackTargetingDetails) == false)
        {
            return false;
        }

        CreatePrefab(attackTargetingDetails);

        // only damage if in range, but still reset cooldown.
        if (attacker.MeleeAttackRange > Helpers.GetDistanceBetweenCharacters(attackTargetingDetails.Attacker, attackTargetingDetails.Target))
        {
            attackTargetingDetails.Target.TakeDamage(CalculateDamage(attackTargetingDetails.Attacker), attackTargetingDetails.Attacker);
        }

        return true;
    }

    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails)
    {
        Quaternion rotation = Quaternion.LookRotation(attackTargetingDetails.Target.transform.position - attackTargetingDetails.Attacker.transform.position);
        GameObject inst = GameObject.Instantiate(Prefab, attackTargetingDetails.Attacker.transform.position, rotation, null);
        GameObject.Destroy(inst, 5f);
    }
}