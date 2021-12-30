﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill
{
    public abstract string Name { get; }
    public abstract float Cooldown { get; }
    public abstract bool CanAttackWhileMoving { get; }
    public abstract bool IsLevelable { get; }
    public float LastAttackTime;
    public abstract int ManaCost { get; }
    protected virtual float ExplosionRadius => 0;
    protected GameObject Prefab;
    public virtual HashSet<SkillType> UnlockDependsOn => new HashSet<SkillType>();
    public abstract SkillType Type { get; }
    public abstract float DamageModifier { get; }
    protected virtual bool ShowsDecal => false;
    protected virtual Item Ammo => null;

    public int Level
    {
        get
        {
            if (!IsLevelable)
            {
                return 1;
            }

            return GameState.Data.SkillLevels.ContainsKey(Type) ? GameState.Data.SkillLevels[Type] : 0;
        }
    }

    public Sprite Icon
    {
        get { return Constants.Icons[this.Name.Replace(" ", "")]; }
    }
    private Sprite _icon;
    protected abstract void CreatePrefab(AttackTargetingDetails attackTargetingDetails);

    public Skill()
    {
        Prefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Skills}/{Name}");
    }

    public virtual bool Activate(Character attacker, AttackTargetingDetails targetingDetails)
    {
        if (Level == 0)
        {
            return false;
        }

        if (attacker.Mana < ManaCost)
        {
            return false;
        }

        if (targetingDetails.Target == null)
        {
            return false;
        }

        if (Time.time - LastAttackTime < Cooldown)
        {
            return false;
        }

        if (Ammo != null && attacker is Player)
        {
            Item item = ((Player)attacker).Inventory.GetItemByName(Ammo.Name);
            if (item == null)
            {
                return false;
            }

            ((Player)attacker).Inventory.RemoveItem(item.Id, 1);
        }

        LastAttackTime = Time.time;
        attacker.Mana -= ManaCost;

        return true;
    }

    public virtual void DealDamage(Character attacker, Character target, GameObject projectile)
    {
        target?.TakeDamage(CalculateDamage(attacker), attacker);
        Explode(attacker, projectile.transform.position);
    }

    public virtual bool IsCollisionTarget(Character attacker, GameObject collision)
    {
        if (collision.transform.parent == null)
        {
            return false;
        }

        // Get Character from parent because collider is always on the body object.
        if (collision.transform.parent.TryGetComponent<Character>(out Character character))
        {
            if (attacker.Enemies.Contains(character.Allegiance))
            {
                return true;
            }
        }

        return false;
    }

    public virtual void Explode(Character attacker, Vector3 position)
    {
        if (ExplosionRadius < .01f)
        {
            return;
        }

        Collider[] hits = Physics.OverlapSphere(position, ExplosionRadius);
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
        return (int)((float)attacker.Damage * DamageModifier);
    }

    protected virtual void CreateGroundEffects(Character attacker, Vector3 position) { }

    protected List<GameObject> SpawnObjectsInCircle(
        GameObject whatToSpawn,
        int howManyToSpawn,
        Vector3 position,
        float spawnRadius,
        float level = Constants.WorldProperties.GroundLevel)
    {
        List<GameObject> objectsCreated = new List<GameObject>();

        for (int i = 0; i < howManyToSpawn; i++)
        {
            Vector3 randomness = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(position.x + randomness.x, level, position.z + randomness.y);

            objectsCreated.Add(GameObject.Instantiate(whatToSpawn, spawnPosition, new Quaternion(), null));
        }

        return objectsCreated;
    }

    public float RemainingCooldown()
    {
        return Mathf.Max(0, Cooldown - (Time.time - LastAttackTime));
    }

    public bool IsOnCooldown()
    {
        return (Time.time < LastAttackTime + Cooldown);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is Skill == false)
        {
            return false;
        }

        return Name == ((Skill)obj).Name;
    }

    protected void DirectProjectile(GameObject projectile, AttackTargetingDetails attackTargetingDetails, float speed)
    {
        Vector3 flyDirection = attackTargetingDetails.Target.transform.position - projectile.transform.position;
        projectile.GetComponent<Rigidbody>().velocity = flyDirection.normalized * speed;
        projectile.GetComponent<Projectile>().Initialize(DealDamage, IsCollisionTarget, CreateGroundEffects, attackTargetingDetails.Attacker);
        projectile.transform.rotation = Quaternion.LookRotation(flyDirection);
    }

    protected virtual GameObject BuildDecal()
    {
        return null;
    }
}
