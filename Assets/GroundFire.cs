using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFire : PersistantAreaEffect
{
    public int Damage;
    public Character Attacker;

    protected override float MinTimeBetweenEffectApplications => 1f;

    public void Setup(int damage, float duration, Character owner, float timeBeforeStart = 0f)
    {
        this.Damage = damage;
        this.Duration = duration;
        this.Attacker = owner;
        this.TimeBeforeStart = timeBeforeStart;

        foreach (ParticleSystem ps in this.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
        }
    }

    protected override void ApplyEffect(GameObject gameObject)
    {
        if (gameObject.transform.parent == null)
        {
            return;
        }

        if (gameObject.transform.parent.TryGetComponent<Character>(out Character character))
        {
            if (Attacker.Enemies.Contains(character.Allegiance))
            {
                character.TakeDamage(Damage, Attacker);
            }
        }
    }

    protected override void Begin()
    {
        base.Begin();

        foreach (ParticleSystem ps in this.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
            var main = ps.main;
            main.duration = Duration - .2f;
            ps.Play();
        }
    }
}
