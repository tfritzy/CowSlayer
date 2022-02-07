using System;
using System.Collections.Generic;

public abstract class StatModifier
{
    public abstract void Apply(Character character);

    public string Id;
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract string ShortDescription { get; }

    public StatModifier(string id)
    {
        this.Id = id;
    }
}