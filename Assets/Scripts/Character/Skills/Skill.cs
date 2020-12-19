using UnityEngine;
using System.Collections;

public abstract class Skill
{
    public abstract string Name { get; }
    public abstract float Cooldown { get; }
    public abstract bool CanAttackWhileMoving { get; }
    public float LastAttackTime;

    protected GameObject AttackPrefab;

    public Skill()
    {
        AttackPrefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Skills}/{Name}");

        if (AttackPrefab == null)
        {
            throw new System.ArgumentNullException($"Missing prefab {Constants.FilePaths.Prefabs.Skills}/{Name}");
        }
    }

    public virtual void Attack(Character attacker, AttackTargetingDetails targetingDetails)
    {
        LastAttackTime = Time.time;
    }

    public virtual void DealDamage(Character attacker, Character target)
    {
        target.TakeDamage(CalculateDamage(attacker), attacker);
    }

    public virtual int CalculateDamage(Character attacker)
    {
        return attacker.Damage;
    }
}
