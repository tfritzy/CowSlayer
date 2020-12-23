using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill
{
    public abstract string Name { get; }
    public abstract float Cooldown { get; }
    public abstract bool CanAttackWhileMoving { get; }
    public float LastAttackTime;
    public abstract int ManaCost { get; }

    protected GameObject AttackPrefab;

    public Skill()
    {
        AttackPrefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Skills}/{Name}");

        if (AttackPrefab == null)
        {
            throw new System.ArgumentNullException($"Missing prefab {Constants.FilePaths.Prefabs.Skills}/{Name}");
        }
    }

    public virtual bool Attack(Character attacker, AttackTargetingDetails targetingDetails)
    {
        if (attacker.Mana < ManaCost)
        {
            return false;
        }

        LastAttackTime = Time.time;
        attacker.Mana -= ManaCost;

        return true;
    }

    public virtual void DealDamage(Character attacker, Character target)
    {
        target.TakeDamage(CalculateDamage(attacker), attacker);
    }

    public virtual void Explode(Character attacker, Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Character>(out Character character))
            {
                if (attacker.Enemies.Contains(character.Allegiance))
                {
                    character.TakeDamage(CalculateDamage(attacker), attacker);
                }
            }
        }
    }

    public virtual int CalculateDamage(Character attacker)
    {
        return attacker.Damage;
    }
}
