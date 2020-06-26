using UnityEngine;
using System.Collections;

public class DamageItemEffect : IntItemEffect
{
    public override string Name => "Damage";
    public override string Description => $"Increases Damage By {this.Value}";
    public override void ApplyEffect(Character character)
    {
        character.Damage += Value;
    }
}
