using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Stick : Weapon
{
    public override string Name => "Stick";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override List<ItemEffect> EffectPool => new List<ItemEffect>() { new DamageItemEffect(1, 3) };
    protected override int NumEffects => 1;
}

