using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public delegate void DamageEnemy(Character attacker, Character target, GameObject projectile);
    private DamageEnemy damageEnemy;
    public delegate bool IsCollisionTarget(Character attacker, GameObject collision);
    private IsCollisionTarget isCollisionTarget;
    public delegate void CreateGroundEffects(Character attacker, Vector3 position);
    private CreateGroundEffects createGroundEffects;
    private Character attacker;
    private const string trailGroupName = "Trail";
    private const string leftoverGround = "LeftoverGroundParticles";
    private Transform explosionChild;

    public void Initialize(
        DamageEnemy damageEnemyHandler,
        IsCollisionTarget isCollisionTarget,
        CreateGroundEffects createGroundEffects,
        Character attacker,
        bool explodesOnGroundContact = false)
    {
        this.damageEnemy = damageEnemyHandler;
        this.isCollisionTarget = isCollisionTarget;
        this.createGroundEffects = createGroundEffects;
        this.attacker = attacker;

        explosionChild = transform.Find("Explosion");
        TriggerAllParticleSystems(explosionChild, false);
        TriggerAllParticleSystems(transform.Find(leftoverGround), false);
        GameObject.Destroy(this.gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other?.gameObject == null)
        {
            return;
        }

        if (isCollisionTarget(attacker, other.gameObject))
        {
            damageEnemy(attacker, other.transform.parent.GetComponent<Character>(), this.gameObject);
            DetachParticles(transform.Find(trailGroupName));
            PlayExplosion();
            createGroundEffects(attacker, this.transform.position);
            GameObject.Destroy(this.gameObject);
        }
    }

    private void DetachParticles(Transform particleGroup)
    {
        if (particleGroup == null)
        {
            return;
        }

        particleGroup.parent = null;
        TriggerAllParticleSystems(particleGroup, false);
        GameObject.Destroy(particleGroup.gameObject, 1f);
    }

    private void PlayExplosion()
    {
        if (explosionChild == null)
        {
            return;
        }

        TriggerAllParticleSystems(explosionChild, true);
        explosionChild.parent = null;
        GameObject.Destroy(explosionChild.gameObject, 10f);
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