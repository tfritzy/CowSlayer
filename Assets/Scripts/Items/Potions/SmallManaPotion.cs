using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallManaPotion : ManaPotion
{
    public override string Name => "Small Mana Potion";
    protected override Effect PrimaryEffectPrefab => new SmallManaRestore();
}
