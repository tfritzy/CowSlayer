using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowardsObject : MonoBehaviour
{
    private const float TurnRate = 3f;
    private GameObject TargetObject;
    private const float collectionRadius = 1f;
    public delegate void RewardLogic();
    private RewardLogic rewardLogicHandler;
    private float startTime;
    private float MovementSpeed;

    public void SetTarget(GameObject Target, RewardLogic rewardLogic)
    {
        this.TargetObject = Target;
        Vector3 currentPosition = this.transform.position;
        this.transform.position = currentPosition;
        GenerateInitialVelocity();
        this.rewardLogicHandler = rewardLogic;
        startTime = Time.time;
    }

    void Update()
    {
        MoveTowardsObject();
        MovementSpeed = (Time.time - startTime) * 4f;
    }

    private void GenerateInitialVelocity()
    {
        Vector2 pointsTowardsTarget = TargetObject.transform.position - this.transform.position;
        pointsTowardsTarget = Quaternion.AngleAxis(Random.Range(-20, 20), Vector3.up) * pointsTowardsTarget;
        this.GetComponent<Rigidbody>().velocity = pointsTowardsTarget.normalized * MovementSpeed;
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
