using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistantAreaEffect : MonoBehaviour
{
    public float TimeBeforeStart;
    public bool Enabled = false;
    public float Duration;
    public GameObject Decal;
    protected abstract float MinTimeBetweenEffectApplications { get; }
    protected Dictionary<GameObject, float> recentHits;
    protected abstract void ApplyEffect(GameObject gameObject);
    private float birthTime;
    private const float fadeOutTime = 0.5f;

    void Update()
    {
        if (Enabled == false && Time.time > birthTime + TimeBeforeStart)
        {
            Begin();
        }

        if (Time.time > birthTime + Duration - fadeOutTime)
        {
            foreach (ParticleSystem ps in this.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Stop();
            }
        }
    }

    void Start()
    {
        birthTime = Time.time;
        this.Decal = this.transform.Find("Decal")?.gameObject;
        Decal.GetComponent<Decal>().Setup(100, 100);
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
        PositionDecal(Decal);
        GameObject.Destroy(this.gameObject, Duration + .1f);
        birthTime = Time.time;
        Decal.GetComponent<Decal>().Setup(Duration - fadeOutTime, fadeOutTime);
        Decal.transform.parent = null;
    }

    protected void PositionDecal(GameObject decal)
    {
        if (decal == null)
        {
            return;
        }

        Helpers.PlaceDecalOnGround(this.transform.position, decal);
    }
}
