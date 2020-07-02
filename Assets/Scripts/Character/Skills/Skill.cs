using UnityEngine;
using System.Collections;

public abstract class Skill
{
    public int Damage;
    public abstract string Name { get; }

    public Skill()
    {

    }

    public abstract void Attack(Character attacker, Vector3 direction);
}
