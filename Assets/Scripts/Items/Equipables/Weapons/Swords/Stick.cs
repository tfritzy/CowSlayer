using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Stick : Weapon
{
    public override string Name => "Stick";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override ItemEffect PrimaryEffectPrefab => new DamageItemEffect(1, 3);
    protected override List<ItemEffect> SecondaryEffectPool => new List<ItemEffect>() { };
    protected override int NumSecondaryEffects => 0;
}

