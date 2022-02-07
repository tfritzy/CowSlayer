
using System.Collections.Generic;

public class GoldRing : Ring
{
    public GoldRing(int level) : base(level)
    {
    }

    public override string Name => "Gold Ring";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Ring;
    public override ItemRarity Rarity => ItemRarity.Rare;
}