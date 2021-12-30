using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCommandUp : MonoBehaviour
{
    private float lastTriggerTime;
    public void Attack()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .1f)
        {
            return;
        }

        transform.parent.GetComponent<Character>().Attack();
        lastTriggerTime = Time.time;
    }

    public void TriggerPrimaryAttack()
    {
        // Skip duplicate events.
        if (Time.time - lastTriggerTime < .1f)
        {
            return;
        }

        transform.parent.GetComponent<Character>().TriggerPrimaryAttack();
        lastTriggerTime = Time.time;
    }
}
