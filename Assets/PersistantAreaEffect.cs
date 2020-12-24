using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistantAreaEffect : MonoBehaviour
{
    protected abstract float MinTimeBetweenEffectApplications { get; }
    protected Dictionary<GameObject, float> recentHits;

    void Start()
    {
        recentHits = new Dictionary<GameObject, float>();
    }

    protected abstract void ApplyEffect(GameObject gameObject);

    private void OnTriggerStay(Collider other)
    {
        if (recentHits.ContainsKey(other.gameObject))
        {
            if (Time.time - recentHits[other.gameObject] > MinTimeBetweenEffectApplications)
            {
                ApplyEffect(other.gameObject);
                recentHits[other.gameObject] = Time.time;
            }
        }
        else
        {
            ApplyEffect(other.gameObject);
            recentHits[other.gameObject] = Time.time;
        }
    }
}
