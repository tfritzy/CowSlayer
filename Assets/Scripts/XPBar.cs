using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBar : MonoBehaviour
{
    public float FillPercentage;
    protected Transform FillBar;
    private float scale;

    void Start()
    {
        this.FillBar = this.transform.Find("FillBar").transform;
    }

    public void SetFillScale(float newFillPercentage)
    {
        this.FillPercentage = newFillPercentage;
        this.FillBar.localScale = new Vector3(this.FillPercentage, 1f, 1f);
    }
}
