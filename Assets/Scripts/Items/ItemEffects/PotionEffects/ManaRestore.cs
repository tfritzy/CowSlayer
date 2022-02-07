using UnityEngine;
using System.Collections;

public abstract class ManaRestore : FlatStatModifier
{
    public ManaRestore(int value, string id) : base(value, id)
    {

    }

    public override string Description => $"Restores {Value} mana";
    public override string ShortDescription => $"Restores {Value} mana";

    public override void Apply(Character character)
    {
        character.Heal(Value);
    }
}
