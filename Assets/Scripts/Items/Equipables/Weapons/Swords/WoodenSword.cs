using System.Collections.Generic;

public class WoodenSword : Sword
{
    public WoodenSword(int level) : base(level)
    {
    }

    public override string Name => "Wooden Sword";
    public override ItemRarity Rarity => RarityFromId(this.Id);
}