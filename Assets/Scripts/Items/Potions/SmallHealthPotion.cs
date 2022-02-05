
using System.Collections.Generic;

public class SmallHealthPotion : HealthPotion
{
    private string name = "Small Health Potion";
    public override string Name => name;
    public override StatModifier PrimaryAttribute => new SmallHealthRestore(this.Id);
    public override ItemRarity Rarity => ItemRarity.Common;
}