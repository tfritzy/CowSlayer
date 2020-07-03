using UnityEngine;
using System.Collections;

public abstract class Skill
{
    public abstract string Name { get; }
    public virtual void Attack(Character attacker, AttackTargetingDetails targetingDetails)
    {
        LastAttackTime = Time.time;
    }
    public abstract float Cooldown { get; }
    public abstract bool CanAttackWhileMoving { get; }
    public float LastAttackTime;
    public virtual void DealDamage(Character attacker, Character target)
    {
        target.TakeDamage(CalculateDamage(attacker), attacker);
    }
    public virtual int CalculateDamage(Character attacker)
    {
        return attacker.Damage;
    }
}
