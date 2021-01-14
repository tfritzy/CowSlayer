using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowardsObject : MonoBehaviour
{
    private float TurnRate;
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
        this.GetComponent<Collider>().enabled = false;
        this.rewardLogicHandler = rewardLogic;
        startTime = Time.time;
    }

    void Update()
    {
        MoveTowardsObject();
        MovementSpeed = Mathf.Min((Time.time - startTime) * 15f + 2f, 10f);
        TurnRate = (Time.time - startTime) * 5f + 1;
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
                Helpers.Destroy(this.gameObject);
            }

            Vector3 currentDirection = this.GetComponent<Rigidbody>().velocity;
            Vector3 newDirection = Vector3.RotateTowards(currentDirection, targetDirection, TurnRate * deltaTime, 100);
            this.GetComponent<Rigidbody>().velocity = newDirection.normalized * MovementSpeed;
            lastVelocityCheckTime = Time.time;
        }
    }
}
