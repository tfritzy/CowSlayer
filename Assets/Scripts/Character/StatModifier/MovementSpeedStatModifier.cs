using System;

public class MovementSpeedStatModifier : MultiplicativeStatModifer
{
    public override string Name => "Damage";
    public override string Description => $"Increases movement speed by {this.PercentModifier}%";
    public override string ShortDescription => $"+{this.PercentModifier}% movement speed";

    public MovementSpeedStatModifier(float value, string id) : base(value, id)
    {
    }

    public override void ApplyModifier(Character character)
    {
        character.MovementSpeed *= (1 + this.PercentModifier);
    }
}