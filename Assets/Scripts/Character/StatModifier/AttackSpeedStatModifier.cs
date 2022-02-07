using System;

public class AttackSpeedStatModifier : FlatStatModifier
{
    public override string Name => "Attack speed";
    public override string Description => $"Increases attack speed by {Value}%";
    public override string ShortDescription => $"+{Value}% attack speed";

    public AttackSpeedStatModifier(string id, float power) : base(id, power) { }

    public override void Apply(Character character)
    {
        character.AttackSpeedPercent += this.Value;
    }
}