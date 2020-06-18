using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropContainer : MonoBehaviour
{
    public Drop drop;
    protected float lastPickupAttemptTime;
    protected const float minTimeOnGround = .5f;
    protected float birthTime;

    public void SetDrop(Drop drop)
    {
        this.drop = drop;
        lastPickupAttemptTime = Time.time;
        birthTime = Time.time;
        drop.SetModel(transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Time.time < lastPickupAttemptTime + .5f)
        {
            return;
        }

        if (Time.time < birthTime + minTimeOnGround)
        {
            return;
        }

        if (!other.CompareTag(Constants.Tags.Player))
        {
            return;
        }

        bool success = drop.GiveDropToPlayer(other.GetComponent<Player>());
        if (success)
        {
            Destroy(this.gameObject);
        }
        
        lastPickupAttemptTime = Time.time;
    }
}
