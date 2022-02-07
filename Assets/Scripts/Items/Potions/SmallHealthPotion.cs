
using System.Collections.Generic;

public class SmallHealthPotion : HealthPotion
{
    private string name = "Small Health Potion";
    public override string Name => name;
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            new SmallHealthRestore(this.Id),
        };
    }
}