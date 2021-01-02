using UnityEngine;
using System.Collections;

public abstract class ManaRestore : Effect
{
    public abstract int Value { get; }

    public ManaRestore()
    {
    }

    public override string Description => $"Restores {Value} Mana";
    public override string ShortDescription => $"Restores {Value} Mana";

    public override void Apply(Character character)
    {
        character.Mana += Value;
    }
}
