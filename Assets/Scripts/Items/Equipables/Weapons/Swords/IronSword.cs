using System.Collections.Generic;

public class IronSword : Sword
{
    private ItemRarity rarity;
    public IronSword(int level) : base(level)
    {
        this.rarity = RarityFromId(this.Id);
    }

    public override string Name => "Iron Sword";
    public override ItemRarity Rarity => this.rarity;
}