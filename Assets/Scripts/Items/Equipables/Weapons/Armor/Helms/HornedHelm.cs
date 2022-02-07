
using System.Collections.Generic;

public class HornedHelm : Helm
{
    public HornedHelm(int level) : base(level)
    {
    }

    public override string Name => "Horned Helm";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Head;
    public override ItemRarity Rarity => ItemRarity.Uncommon;
}