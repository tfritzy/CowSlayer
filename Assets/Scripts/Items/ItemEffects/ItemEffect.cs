using UnityEngine;
using System.Collections;

public abstract class Effect
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract string ShortDescription { get; }
    public abstract void Apply(Character character);
}
