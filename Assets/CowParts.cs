using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowParts : MonoBehaviour
{
    void Start()
    {
        foreach (Transform bodyPart in this.transform)
        {
            bodyPart.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere * 15;
            bodyPart.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 360;

            Destroy(bodyPart.gameObject, 5f);
        }

        this.transform.DetachChildren();

        Destroy(this.gameObject);
    }
}
