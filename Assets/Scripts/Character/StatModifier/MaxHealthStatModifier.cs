using System;

public class MaxHealthStatModifier : FlatStatModifier
{
    public override string Name => "Max health";
    public override string Description => $"Increases max health by {Value}";
    public override string ShortDescription => $"+{Value} max health";

    public MaxHealthStatModifier(int minValue, int maxValue, string id) : base(minValue, maxValue, id) { }

    public override void ApplyModifier(Character character)
    {
        character.MaxHealth += this.Value;
        character.Health += this.Value;
    }
}