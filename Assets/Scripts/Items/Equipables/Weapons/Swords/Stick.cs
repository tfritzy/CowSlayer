using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Stick : Sword
{
    public override string Name => "Stick";
    public override ItemRarity Rarity => ItemRarity.Common;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(1, 3, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => new List<StatModifier>() { };
    protected override int NumSecondaryEffects => 0;
}

