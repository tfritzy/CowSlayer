using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallManaPotion : ManaPotion
{
    private string name = "Small Mana Potion";
    public override string Name => name;
    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            new SmallManaRestore(this.Id),
        };
    }
}
