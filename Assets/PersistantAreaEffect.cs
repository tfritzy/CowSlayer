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
        this.Decal = this.transform.Find("Decal")?.gameObject;
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
        Destroy(this.gameObject, Duration + .1f);
    }

    protected void PositionDecal(GameObject decal)
    {
        if (decal == null)
        {
            return;
        }

        Ray ray = new Ray(this.transform.position, Vector3.down);
        Physics.Raycast(ray, out RaycastHit hitInfo);
        decal.transform.position = hitInfo.point;
        decal.transform.forward = hitInfo.normal * -1f;
        decal.transform.rotation = decal.transform.rotation * Quaternion.AngleAxis(Random.Range(0, 360), decal.transform.up);
        decal.transform.parent = null;
        Destroy(decal, 5f);
    }
}
