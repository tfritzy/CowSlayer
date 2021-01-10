using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCommandUp : MonoBehaviour
{
    public void Attack()
    {
        transform.parent.GetComponent<Character>().Attack();
    }
}
