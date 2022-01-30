using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body
{
    public GameObject OffHand;
    public GameObject MainHand;
    public Transform Transform;
    public Animator Animator;
    public CapsuleCollider Collider;
    public Vector2 VerticalBounds
    {
        get
        {
            Vector2 bounds = new Vector2(float.MaxValue, float.MinValue);
            foreach (MeshRenderer mr in Transform.GetComponentsInChildren<MeshRenderer>())
            {
                if (mr.bounds.max.y > bounds.y)
                {
                    bounds.y = mr.bounds.max.y;
                }

                if (mr.bounds.min.y < bounds.x)
                {

                    bounds.x = mr.bounds.min.y;
                }
            }
            return bounds;
        }
    }

    public Body(Transform self)
    {
        if (self == null)
        {
            throw new System.ArgumentNullException("Every character must have a body. It must be named 'Body'");
        }

        Transform = self;
        this.OffHand = Helpers.FindDeepChild(self, "LeftHand", true)?.gameObject;
        this.MainHand = Helpers.FindDeepChild(self, "RightHand", true)?.gameObject;
        this.Animator = self.GetComponent<Animator>();
        this.Collider = self.GetComponent<CapsuleCollider>();

        if (self.gameObject.TryGetComponent<PassCommandUp>(out _) == false)
        {
            self.gameObject.AddComponent<PassCommandUp>();
        }
    }

    private Vector3 forward;
    public Vector3 Forward
    {
        get
        {
            forward = this.Transform.forward;
            forward.y = 0;
            return forward;
        }
    }

    public Quaternion Rotation
    {
        get
        {
            return Quaternion.LookRotation(this.Forward);
        }
    }
}

