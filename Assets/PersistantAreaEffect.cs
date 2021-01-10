using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistantAreaEffect : MonoBehaviour
{
    public float TimeBeforeStart;
    public bool Enabled = false;
    public float Duration;
    protected abstract float MinTimeBetweenEffectApplications { get; }
    protected Dictionary<GameObject, float> recentHits;
    protected abstract void ApplyEffect(GameObject gameObject);
    private float birthTime;

    void Update()
    {
        if (Enabled == false && Time.time > birthTime + TimeBeforeStart)
        {
            Begin();
        }
    }

    void Start()
    {
        birthTime = Time.time;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Enabled == false)
        {
            return;
        }

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

    protected virtual void Begin()
    {
        Enabled = true;
        recentHits = new Dictionary<GameObject, float>();
        Destroy(this.gameObject, Duration + .1f);
    }
}
