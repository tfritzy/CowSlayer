using System;

public class MaxHealthStatModifier : FlatStatModifier
{
    public override string Name => "Max health";
    public override string Description => $"Increases max health by {Value}";
    public override string ShortDescription => $"+{Value} max health";

    public MaxHealthStatModifier(string id, float power) : base(id, power) { }

    public override void Apply(Character character)
    {
        character.MaxHealth += this.Value;
        character.Health += this.Value;
    }
}