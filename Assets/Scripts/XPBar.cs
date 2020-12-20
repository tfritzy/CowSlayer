using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBar : MonoBehaviour
{
    public float FillPercentage;
    public Transform FillBar;
    private float scale;
    public bool FillXDirection = true;

    public void SetFillScale(float newFillPercentage)
    {
        this.FillPercentage = newFillPercentage;

        if (this.FillPercentage > 1)
        {
            this.FillPercentage = 1;
        }

        if (this.FillPercentage < 0)
        {
            this.FillPercentage = 0;
        }

        if (FillXDirection)
        {
            this.FillBar.localScale = new Vector3(this.FillPercentage, 1f, 1f);
        }
        else
        {
            this.FillBar.localScale = new Vector3(1f, this.FillPercentage, 1f);
        }

    }
}
