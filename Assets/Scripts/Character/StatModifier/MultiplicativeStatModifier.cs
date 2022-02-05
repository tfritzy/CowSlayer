using System;

public abstract class MultiplicativeStatModifer : StatModifier
{
    public float PercentModifier;

    public MultiplicativeStatModifer(float value, string id) : base(id)
    {
        this.PercentModifier = value;
    }
}