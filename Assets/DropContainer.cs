using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropContainer : MonoBehaviour
{
    public Drop drop;
    protected float lastPickupAttemptTime;

    public void SetDrop(Drop drop)
    {
        this.drop = drop;
        lastPickupAttemptTime = Time.time;
        drop.SetModel(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < lastPickupAttemptTime + .5f)
        {
            return;
        }

        lastPickupAttemptTime = Time.time;

        if (other.CompareTag(Constants.Tags.Player))
        {
            bool success = drop.GiveDropToPlayer(other.GetComponent<Player>());

            if (success)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
