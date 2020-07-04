using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public Transform Owner;
    public float FillPercentage;
    protected Transform FillBar;
    private float scale;

    void Start()
    {
        this.FillBar = this.transform.Find("FillBar").transform;
    }

    void Update()
    {
        if (Owner == null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        transform.position = Constants.Persistant.Camera.WorldToScreenPoint (Owner.position) + new Vector3(0, 150 * scale);
    }

    public void SetOwner(Transform owner){
        this.Owner = owner;
        this.scale = owner.localScale.x;
    }

    public void SetFillScale(float newFillPercentage)
    {
        this.FillPercentage = newFillPercentage;
        this.FillBar.localScale = new Vector3(this.FillPercentage, 1f, 1f);
    }
}
