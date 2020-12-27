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
    protected virtual float ExplosionRadius => 0;
    protected GameObject AttackPrefab;
    public virtual HashSet<SkillType> UnlockDependsOn => new HashSet<SkillType>();
    public abstract SkillType Type { get; }

    public Sprite Icon {
        get {
            if (_icon == null)
            {
                _icon = Resources.Load<Sprite>(IconFilePath);
            }

            return _icon;
        }
    }
    protected abstract string IconFilePath {get;}
    private Sprite _icon;

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

    public virtual void DealDamage(Character attacker, Character target, GameObject projectile)
    {
        target?.TakeDamage(CalculateDamage(attacker), attacker);
        Explode(attacker, projectile.transform.position);
    }

    public virtual bool IsCollisionTarget(Character attacker, GameObject collision)
    {
        if (collision.TryGetComponent<Character>(out Character character))
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
        return attacker.Damage;
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
}
