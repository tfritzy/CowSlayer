using System;

public class ArmorStatModifier : FlatStatModifier
{
    public override string Name => "Armor";
    public override string Description => $"Increases armor by {Value}";
    public override string ShortDescription => $"+{Value} armor";

    public ArmorStatModifier(string id, float power) : base(id, power) { }

    public override void Apply(Character character)
    {
        character.Armor += this.Value;
    }
}