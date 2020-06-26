using UnityEngine;
using System.Collections;

public abstract class IntItemEffect : ItemEffect
{
    public int LowValue;
    public int HighValue;
    public int Value;

    public IntItemEffect(int low, int high)
    {
        this.LowValue = low;
        this.HighValue = high;
    }

    public override void RollRandomValue()
    {
        this.Value = Random.Range(LowValue, HighValue + 1);
    }
}
