using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixWingsProjectile : Projectile
{
    const float maxRotationDegreesPerSecond = 200;
    const float startDelaySeconds = .5f;

    protected override void UpdateLoop()
    {
        if (Time.time < this.birthTime + startDelaySeconds)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.FromToRotation(this.transform.position, this.target.transform.position);
        this.Rigidbody.velocity = Quaternion.RotateTowards(this.transform.rotation, targetRotation, maxRotationDegreesPerSecond * Time.deltaTime) * this.Rigidbody.velocity;
    }
}
