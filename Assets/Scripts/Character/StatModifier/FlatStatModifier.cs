using System;

public abstract class FlatStatModifier : StatModifier
{
    public int Value;

    public FlatStatModifier(int value, string id) : base(id)
    {
        this.Value = value;
    }

    public FlatStatModifier(int minValue, int maxValue, string id) : base(id)
    {
        // Roll a random percent of max value, with the randomness tethered to the id.
        // Rolling a percent of max ensures that later changing the range of values will not make a good item bad.
        // Adding 1 fixes the problem where it would be virtually impossible to get an item to roll the max value.
        Random random = new Random(id.GetHashCode());
        double percentOfMax = random.NextDouble();
        this.Value = (int)(minValue + (maxValue - minValue) * percentOfMax) + 1;
    }
}