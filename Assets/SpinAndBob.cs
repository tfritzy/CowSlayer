using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAndBob : MonoBehaviour
{
    public float AverageSpinSpeed;
    public float AverageBobSpeed;
    public float BobHeight;

    private Rigidbody rb;
    private Vector3 angularVelocity;
    private Vector3 position;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        angularVelocity = Vector3.zero;
    }

    float tickSpeed = .1f;
    float lastTickTime;
    void Update()
    {
        if (Time.time > lastTickTime + tickSpeed)
        {
            angularVelocity.y = Mathf.Lerp(AverageSpinSpeed * .5f, AverageSpinSpeed * 1.5f, Time.time);
            rb.angularVelocity = angularVelocity;

            Vector3 diff = transform.parent.position - this.transform.position;
            Vector3 velocity = diff.normalized * diff.magnitude * 3;
            velocity.y = GetVerticalVelocity();
            rb.velocity = velocity;
            lastTickTime = Time.time;
        }
    }

    bool goingDown = false;
    float GetVerticalVelocity()
    {
        if (goingDown && transform.position.y < BobHeight - .25f)
        {
            goingDown = false;
        }

        if (goingDown == false && transform.position.y > BobHeight + .25f)
        {
            goingDown = true;
        }

        if (goingDown)
        {
            return -AverageBobSpeed;
        }
        else
        {
            return AverageBobSpeed;
        }
    }
}
