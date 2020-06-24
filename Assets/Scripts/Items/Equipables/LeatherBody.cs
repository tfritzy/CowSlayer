
public class LeatherBody : EquipableItem 
{
    public override string Name => "Leather Body";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Chest;
    public override ItemRarity Rarity => ItemRarity.Common;
}