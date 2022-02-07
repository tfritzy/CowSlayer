using System;

public class MagicAffinityStatModifier : FlatStatModifier
{
    public override string Name => "Magic affinity";
    public override string Description => $"Increases magic affinity by {Value}";
    public override string ShortDescription => $"+{Value} magic affinity";

    public MagicAffinityStatModifier(string id, float power) : base(id, power) { }

    public override void Apply(Character character)
    {
        character.MagicAffinity += this.Value;
    }
}