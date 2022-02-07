
using System.Collections.Generic;

public class LeatherBody : Torso
{
    public LeatherBody(int level) : base(level)
    {
    }

    public override string Name => "Leather Body";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Chest;
    public override ItemRarity Rarity => ItemRarity.Common;
}