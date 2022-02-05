
using System.Collections.Generic;

public abstract class HealthPotion : Potion
{

    private ItemRarity rarity = ItemRarity.Common;
    public override ItemRarity Rarity => rarity;
}