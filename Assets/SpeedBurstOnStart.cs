using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBurstOnStart : MonoBehaviour
{
    public void Begin(float speed)
    {
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * speed, ForceMode.VelocityChange);
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(
            Random.Range(-360, 360) * Mathf.Rad2Deg,
            Random.Range(-360, 360) * Mathf.Rad2Deg,
            Random.Range(-360, 360) * Mathf.Rad2Deg);
    }
}
