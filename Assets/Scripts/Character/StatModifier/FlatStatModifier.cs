using System;

public abstract class FlatStatModifier : StatModifier
{
    public int Value;

    public FlatStatModifier(int value, string id) : base(id)
    {
        this.Value = value;
    }

    public FlatStatModifier(string id, float power) : base(id)
    {
        // TODO: set value according to power.
        this.Value = 100000;
    }
}