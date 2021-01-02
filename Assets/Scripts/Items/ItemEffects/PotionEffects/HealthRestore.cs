using UnityEngine;
using System.Collections;

public abstract class HealthRestore : Effect
{
    public abstract int Value { get; }

    public HealthRestore()
    {
    }

    public override string Description => $"Restores {Value} Health";
    public override string ShortDescription => $"Restores {Value} Health";

    public override void Apply(Character character)
    {
        character.Heal(Value);
    }
}
