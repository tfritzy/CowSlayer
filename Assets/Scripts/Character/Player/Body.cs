using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body
{
    public GameObject OffHand;
    public GameObject MainHand;
    public Transform Transform;
    public Animator Animator;

    public Body(Transform self)
    {
        if (self == null)
        {
            throw new System.ArgumentNullException("Every character must have a body. It must be named 'Body'");
        }

        Transform = self;
        this.OffHand = self.Find("LeftHand")?.gameObject;
        this.MainHand = self.Find("RightHand")?.gameObject;
        this.Animator = self.GetComponent<Animator>();
    }
}

