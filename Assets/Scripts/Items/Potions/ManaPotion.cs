
using System.Collections.Generic;

public abstract class ManaPotion : Potion
{

    private ItemRarity rarity = ItemRarity.Common;
    public override ItemRarity Rarity => rarity;
}