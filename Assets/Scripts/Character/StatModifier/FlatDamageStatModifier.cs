using System;

public class FlatDamageStatModifier : FlatStatModifier
{
    public override string Name => "Damage";
    public override string Description => $"Increases damage by {Value}";
    public override string ShortDescription => $"+{Value} damage";

    public FlatDamageStatModifier(int minValue, int maxValue, string id) : base(minValue, maxValue, id) { }

    public override void ApplyModifier(Character character)
    {
        character.Damage += this.Value;
    }
}