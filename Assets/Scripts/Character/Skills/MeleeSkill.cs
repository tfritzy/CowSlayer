using UnityEngine;

public abstract class MeleeSkill : Skill 
{
    public override void Attack(Character attacker, AttackTargetingDetails attackTargetingDetails)
    {
        attackTargetingDetails.Target.TakeDamage(CalculateDamage(attacker), attacker);
        base.Attack(attacker, attackTargetingDetails);
    }
}