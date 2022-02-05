using UnityEngine;
using System.Collections;

public class DamageItemEffect : IntItemEffect
{

    private string name = "Damage";
    public override string Name => name;

    private string description = $"Increases Damage By {this.Value}";
    public override string Description => description;
    public override void ApplyEffect(Character character)
    {
        character.Damage += Value;
    }
}
