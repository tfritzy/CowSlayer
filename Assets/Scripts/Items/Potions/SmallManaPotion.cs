using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallManaPotion : ManaPotion
{
    private string name = "Small Mana Potion";
    public override string Name => name;
    public override StatModifier PrimaryAttribute => new SmallManaRestore(this.Id);
}
