using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixWingsProjectile : Projectile
{
    float maxRotationDegreesPerSecond = 300;
    Vector3 upwardsVelocity = new Vector3(0, 20f, 0);
    const float startDelaySeconds = .9f;
    float movementSpeed;
    Vector3 targetPosition;

    protected override void StartLogic()
    {
        this.Rigidbody.velocity = this.Rigidbody.velocity + upwardsVelocity;
    }

    protected override void UpdateLoop()
    {
        if (Time.time < this.birthTime + startDelaySeconds)
        {
            return;
        }

        MoveTowardsObject();

        if (target == null && (this.transform.position - targetPosition).magnitude < .2f)
        {
            Destroy(this.gameObject);
        }

        maxRotationDegreesPerSecond += 50 * Time.deltaTime;
    }

    private void MoveTowardsObject()
    {
        if (target != null)
        {
            targetPosition = target.transform.position;
        }

        movementSpeed = Mathf.Min(20f, (Time.time - birthTime) * 15 + 8f);

        Vector3 targetDirection = targetPosition - this.transform.position;
        Vector3 currentDirection = this.GetComponent<Rigidbody>().velocity;
        // Vector3 newDirection = Vector3.RotateTowards(currentDirection, targetDirection, maxRotationDegreesPerSecond * Mathf.Deg2Rad * Time.deltaTime, movementSpeed);
        targetDirection = targetDirection.normalized * movementSpeed;
        this.GetComponent<Rigidbody>().velocity = targetDirection;
    }
}
