using UnityEngine;
using System.Collections;

public abstract class HealthRestore : FlatStatModifier
{
    public HealthRestore(int value, string id) : base(value, id)
    {

    }

    public override string Description => $"Restores {Value} health";
    public override string ShortDescription => $"Restores {Value} health";

    public override void ApplyModifier(Character character)
    {
        character.Heal(Value);
    }
}
