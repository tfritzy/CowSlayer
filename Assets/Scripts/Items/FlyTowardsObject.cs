using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowardsObject : MonoBehaviour
{
    private float TurnRate = 2f;
    private GameObject TargetObject;
    private const float collectionRadius = 1f;
    public delegate void RewardLogic();
    private RewardLogic rewardLogicHandler;
    private float startTime;
    private float MovementSpeed;
    private float moveDelay;

    public void SetTarget(GameObject Target, RewardLogic rewardLogic)
    {
        this.TargetObject = Target;
        Vector3 currentPosition = this.transform.position;
        this.transform.position = currentPosition;
        this.GetComponent<Collider>().enabled = false;
        ConstantForce upwardForce = this.gameObject.AddComponent<ConstantForce>();
        upwardForce.force = Vector3.up * 3f;
        this.rewardLogicHandler = rewardLogic;
        startTime = Time.time;
        moveDelay = .2f;
    }

    void Update()
    {
        // Let the object hover for a while before starting to move towards target.
        if (Time.time > startTime + moveDelay)
        {
            MoveTowardsObject();
            MovementSpeed = Mathf.Min((Time.time - startTime) * 4f, 10f);
            TurnRate = (Time.time - startTime) * 2f + 1;
        }
    }

    private float lastVelocityCheckTime;
    private const float deltaTime = .1f;
    private void MoveTowardsObject()
    {
        if (TargetObject == null)
        {
            return;
        }

        if (Time.time > lastVelocityCheckTime + deltaTime)
        {
            Vector3 targetDirection = TargetObject.transform.position - this.transform.position;
            if (targetDirection.magnitude < collectionRadius)
            {
                rewardLogicHandler();
                Destroy(this.gameObject);
            }

            Vector3 currentDirection = this.GetComponent<Rigidbody>().velocity;
            Vector3 newDirection = Vector3.RotateTowards(currentDirection, targetDirection, TurnRate * deltaTime, 100);
            this.GetComponent<Rigidbody>().velocity = newDirection.normalized * MovementSpeed;
            lastVelocityCheckTime = Time.time;
        }
    }
}
