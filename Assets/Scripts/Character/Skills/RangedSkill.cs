using UnityEngine;
using System.Collections;

public abstract class RangedSkill : Skill
{
    private GameObject AttackPrefab;
    protected abstract float MovementSpeed { get; }

    public RangedSkill()
    {
        AttackPrefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Skills}/{Name}");
    }

    public override void Attack(Character attacker, Vector3 direction)
    {
        GameObject projectile = GameObject.Instantiate(AttackPrefab, attacker.transform.position, new Quaternion(), null);
        Vector3 flyDirection = direction - attacker.transform.position;
        projectile.GetComponent<Rigidbody>().velocity = flyDirection.magnitude * MovementSpeed;
    }
}
