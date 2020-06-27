using UnityEngine;
using System.Collections;

public class DamageItemEffect : IntItemEffect
{
    public DamageItemEffect(int low, int high) : base(low, high)
    {
    }

    public override string Name => "Damage";
    public override string Description => $"Increases Damage By {Value}";
    public override string ShortDescription => $"+{Value} Damage";

    public override void Apply(Character character)
    {
        character.Damage += Value;
    }
}
