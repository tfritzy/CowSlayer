using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFire : PersistantAreaEffect
{
    public int Damage;
    public float Duration;
    public Character Attacker;

    protected override float MinTimeBetweenEffectApplications => 1f;

    public void Setup(int damage, float duration, Character owner)
    {
        this.Damage = damage;
        this.Duration = duration;
        this.Attacker = owner;

        foreach (ParticleSystem ps in this.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
            var main = ps.main;
            main.duration = Duration;
            ps.Play();
        }

        Destroy(this.gameObject, Duration + 1f);
    }

    protected override void ApplyEffect(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Character>(out Character character))
        {
            if (Attacker.Enemies.Contains(character.Allegiance))
            {
                character.TakeDamage(Damage, Attacker);
            }
        }
    }
}
