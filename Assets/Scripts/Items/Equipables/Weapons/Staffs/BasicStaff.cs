using System.Collections.Generic;

public class BasicStaff : Staff
{
    private ItemRarity rarity;
    public BasicStaff(int level) : base(level)
    {
        this.rarity = RarityFromId(this.Id);
    }

    public override string Name => "Basic Staff";
    public override ItemRarity Rarity => rarity;
}