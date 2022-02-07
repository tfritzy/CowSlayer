
using System.Collections.Generic;

public class IronPlatelegs : Legging
{
    public IronPlatelegs(int level) : base(level)
    {
    }

    public override string Name => "Iron Platelegs";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Legs;
    public override ItemRarity Rarity => ItemRarity.Common;
}