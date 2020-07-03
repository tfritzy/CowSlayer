using System;
using UnityEngine;

public class Projectile : MonoBehaviour 
{
    public delegate void DamageEnemy(Character attacker, Character target);
    private DamageEnemy damageEnemyHandler;
    private Character attacker;
    private const string trailGroupName = "Trail";

    public void Initialize(DamageEnemy damageEnemyHandler, Character attacker)
    {
        this.damageEnemyHandler = damageEnemyHandler;
        this.attacker = attacker;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character otherCharacter = other.GetComponent<Character>();
        
        if (otherCharacter == null){
            return;
        }

        if (attacker.Enemies.Contains(otherCharacter.Allegiance))
        {
            damageEnemyHandler(attacker, otherCharacter);
            DetachParticles(transform.Find(trailGroupName));
            Destroy(this.gameObject);
        }
    }

    private void DetachParticles(Transform particleGroup)
    {
        particleGroup.parent = null;
        particleGroup.GetComponent<ParticleSystem>().Stop();
        Destroy(particleGroup.gameObject, 1f);
    }
}