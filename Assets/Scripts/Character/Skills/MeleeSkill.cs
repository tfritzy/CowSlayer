using UnityEngine;

public abstract class MeleeSkill : Skill
{
    public override bool Attack(Character attacker, AttackTargetingDetails attackTargetingDetails)
    {
        if (base.Attack(attacker, attackTargetingDetails) == false)
        {
            return false;
        }

        Quaternion rotation = Quaternion.LookRotation(attackTargetingDetails.Target.transform.position - attacker.transform.position);
        GameObject inst = GameObject.Instantiate(AttackPrefab, attacker.transform.position, rotation, null);
        GameObject.Destroy(inst, 5f);
        attackTargetingDetails.Target.TakeDamage(CalculateDamage(attacker), attacker);

        return true;
    }
}