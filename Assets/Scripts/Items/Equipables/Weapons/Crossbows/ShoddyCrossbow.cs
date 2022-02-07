using System.Collections.Generic;

public class ShoddyCrossbow : Crossbow
{
    private ItemRarity rarity;
    public ShoddyCrossbow(int level) : base(level)
    {
        this.rarity = this.RarityFromId(this.Id);
    }

    public override string Name => "Shoddy Crossbow";
    public override ItemRarity Rarity => this.rarity;
}