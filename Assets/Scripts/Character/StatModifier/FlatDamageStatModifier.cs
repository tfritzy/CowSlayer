using System;

public class FlatDamageStatModifier : FlatStatModifier
{
    public override string Name => "Damage";
    public override string Description => $"Increases damage by {Value}";
    public override string ShortDescription => $"+{Value} damage";

    public FlatDamageStatModifier(string id, float power) : base(id, power) { }

    public override void Apply(Character character)
    {
        character.Damage += this.Value;
    }
}