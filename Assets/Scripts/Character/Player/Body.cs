using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body
{
    public GameObject OffHand;
    public GameObject MainHand;
    public Transform Transform;

    public Body(Transform self)
    {
        Transform = self;
        this.OffHand = self.Find("LeftHand")?.gameObject;
        this.MainHand = self.Find("RightHand")?.gameObject;
    }
}

