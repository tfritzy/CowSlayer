using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public delegate void DamageEnemy(Character attacker, Character target);
    private DamageEnemy damageEnemyHandler;
    private Character attacker;
    private const string trailGroupName = "Trail";
    private const string leftoverGround = "LeftoverGroundParticles";
    private Transform explosionChild;
    private bool explodesOnGroundContact;

    public void Initialize(
        DamageEnemy damageEnemyHandler,
        Character attacker,
        bool explodesOnGroundContact = false)
    {
        this.damageEnemyHandler = damageEnemyHandler;
        this.attacker = attacker;
        this.explodesOnGroundContact = explodesOnGroundContact;

        explosionChild = transform.Find("Explosion");
        TriggerAllParticleSystems(explosionChild, false);
        TriggerAllParticleSystems(transform.Find(leftoverGround), false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (explodesOnGroundContact && other.CompareTag(Constants.Tags.Ground))
        {
            this.OnHitTarget(null);
        }

        Character otherCharacter = other.GetComponent<Character>();

        if (otherCharacter == null)
        {
            return;
        }

        if (attacker.Enemies.Contains(otherCharacter.Allegiance))
        {
            this.OnHitTarget(otherCharacter);
        }
    }

    private void OnHitTarget(Character target)
    {
        damageEnemyHandler(attacker, target);
        DetachParticles(transform.Find(trailGroupName));
        TriggerGroundParticles();
        PlayExplosion();
        Destroy(this.gameObject);
    }

    private void DetachParticles(Transform particleGroup)
    {
        particleGroup.parent = null;
        TriggerAllParticleSystems(particleGroup, false);
        Destroy(particleGroup.gameObject, 1f);
    }

    private void PlayExplosion()
    {
        TriggerAllParticleSystems(explosionChild, true);
        explosionChild.parent = null;
        Destroy(explosionChild.gameObject, 1f);
    }

    private void TriggerGroundParticles()
    {
        GameObject groundParticles = transform.Find(leftoverGround)?.gameObject;
        if (groundParticles == null)
        {
            return;
        }

        groundParticles.transform.parent = null;
        Destroy(groundParticles, 4f);

        if (groundParticles == null)
        {
            return;
        }

        groundParticles.GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < UnityEngine.Random.Range(4, 6); i++)
        {
            Vector3 position = groundParticles.transform.position;
            position += new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0, UnityEngine.Random.Range(-0.3f, 0.3f));
            GameObject gp = Instantiate(groundParticles, position, new Quaternion(), groundParticles.transform);
            TriggerAllParticleSystems(gp.transform, true);
        }
    }

    private void TriggerAllParticleSystems(Transform transform, bool start)
    {
        if (transform == null)
        {
            return;
        }

        transform.gameObject.TryGetComponent<ParticleSystem>(out ParticleSystem parentPS);
        parentPS?.Stop();
        foreach (ParticleSystem ps in transform.GetComponentsInChildren<ParticleSystem>())
        {
            if (start)
            {
                ps.Play();
            }
            else
            {
                ps.Stop();
            }
        }
    }
}