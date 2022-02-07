using System.Collections.Generic;

public class SteelSword : Sword
{
    private ItemRarity rarity;
    public SteelSword(int level) : base(level)
    {
        this.rarity = RarityFromId(this.Id);
    }

    public override string Name => "Steel Sword";

    public override ItemRarity Rarity => this.rarity;
}