using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowPart : MonoBehaviour
{
    const float lifeTime = 10f;
    const float sinkTime = 10f;
    float sinkRate;
    float birthTime;
    Collider myCollider;

    void Start()
    {
        birthTime = Time.time;
        float sinkDistance = (this.GetComponent<MeshRenderer>().bounds.extents.y * 2) * 1.1f;
        sinkRate = sinkDistance / sinkTime;
        myCollider = this.GetComponent<Collider>();
    }

    void Update()
    {
        if (Time.time > birthTime + lifeTime)
        {
            if (myCollider != null)
            {
                myCollider.enabled = false;
                this.GetComponent<Rigidbody>().useGravity = false;
            }

            Vector3 position = this.transform.position;
            position.y -= sinkRate * Time.deltaTime;
            this.transform.position = position;
        }

        if (Time.time > birthTime + lifeTime + sinkTime)
        {
            Destroy(this.gameObject);
        }
    }
}
