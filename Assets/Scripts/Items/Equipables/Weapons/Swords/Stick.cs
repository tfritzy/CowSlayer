using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Stick : Sword
{
    public override string Name => "Stick";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(1, 3);
    protected override List<Effect> SecondaryEffectPool => new List<Effect>() { };
    protected override int NumSecondaryEffects => 0;
}

