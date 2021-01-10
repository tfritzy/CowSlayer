using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBurstOnStart : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * Speed, ForceMode.VelocityChange);
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(
            Random.Range(-360, 360) * Mathf.Rad2Deg,
            Random.Range(-360, 360) * Mathf.Rad2Deg,
            Random.Range(-360, 360) * Mathf.Rad2Deg);
    }
}
