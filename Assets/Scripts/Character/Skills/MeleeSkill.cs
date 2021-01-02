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

        return true;
    }

    protected override void CreatePrefab(AttackTargetingDetails attackTargetingDetails)
    {
        Quaternion rotation = Quaternion.LookRotation(attackTargetingDetails.Target.transform.position - attackTargetingDetails.Attacker.transform.position);
        GameObject inst = GameObject.Instantiate(Prefab, attackTargetingDetails.Attacker.transform.position, rotation, null);
        GameObject.Destroy(inst, 5f);
        attackTargetingDetails.Target.TakeDamage(CalculateDamage(attackTargetingDetails.Attacker), attackTargetingDetails.Attacker);
    }
}