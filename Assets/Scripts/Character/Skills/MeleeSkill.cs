using UnityEngine;

public abstract class MeleeSkill : Skill 
{
    public override void Attack(Character attacker, AttackTargetingDetails attackTargetingDetails)
    {
        Quaternion rotation = Quaternion.LookRotation(attackTargetingDetails.Target.transform.position - attacker.transform.position);
        GameObject inst = GameObject.Instantiate(AttackPrefab, attacker.transform.position, rotation, null);
        GameObject.Destroy(inst, 5f);
        attackTargetingDetails.Target.TakeDamage(CalculateDamage(attacker), attacker);
        base.Attack(attacker, attackTargetingDetails);
    }
}