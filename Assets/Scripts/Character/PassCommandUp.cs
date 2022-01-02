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
}
