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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other?.gameObject == null)
        {
            return;
        }

        if (isCollisionTarget(attacker, other.gameObject))
        {
            damageEnemy(attacker, other.GetComponent<Character>(), this.gameObject);
            DetachParticles(transform.Find(trailGroupName));
            TriggerGroundParticles();
            PlayExplosion();
            createGroundEffects(attacker, this.transform.position);
            Destroy(this.gameObject);
        }
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
        Destroy(explosionChild.gameObject, 10f);
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