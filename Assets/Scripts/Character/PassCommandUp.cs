using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCommandUp : MonoBehaviour
{
    private float lastTriggerTime;
    public void AttackAnimTrigger()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .03f)
        {
            return;
        }

        transform.parent.GetComponent<Character>().AttackAnimTrigger();
        lastTriggerTime = Time.time;
    }

    public void BackwardsKicKAnimTrigger()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .03f)
        {
            return;
        }

        transform.parent.GetComponent<Cow>().BackwardsKicKAnimTrigger();
        lastTriggerTime = Time.time;
    }

    public void FinishBackKickAnimTrigger()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .03f)
        {
            return;
        }

        transform.parent.GetComponent<Cow>().FinishBackKickAnimTrigger();
        lastTriggerTime = Time.time;
    }

    public void FinishedEatingGrassAnimTrigger()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .03f)
        {
            return;
        }

        transform.parent.GetComponent<Cow>().FinishedEatingGrassAnimTrigger();
        lastTriggerTime = Time.time;
    }

    public void FinishedWindingUpChargeAnimTrigger()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .03f)
        {
            return;
        }

        transform.parent.GetComponent<Cow>().FinishedWindingUpChargeAnimTrigger();
        lastTriggerTime = Time.time;
    }

    public void FinishedSkiddingToStopAnimTrigger()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .03f)
        {
            return;
        }

        transform.parent.GetComponent<Cow>().FinishedSkiddingToStopAnimTrigger();
        lastTriggerTime = Time.time;
    }

    public void EndFlinchAnimTrigger()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .03f)
        {
            return;
        }

        transform.parent.GetComponent<Cow>().EndFlinchAnimTrigger();
        lastTriggerTime = Time.time;
    }
}
