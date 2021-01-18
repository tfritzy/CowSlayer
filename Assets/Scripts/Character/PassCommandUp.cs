using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCommandUp : MonoBehaviour
{
    private float lastAttackTime;
    public void Attack()
    {
        // Skip duplicate events.
        if (Time.time - lastAttackTime < .1f)
        {
            return;
        }

        transform.parent.GetComponent<Character>().Attack();
        lastAttackTime = Time.time;
    }

    public void TriggerPrimaryAttack()
    {
        // Skip duplicate events.
        if (Time.time - lastAttackTime < .1f)
        {
            return;
        }

        transform.parent.GetComponent<Character>().TriggerPrimaryAttack();
        lastAttackTime = Time.time;
    }
}
