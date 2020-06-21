
public class GoldRing : EquipableItem 
{
    public override string Name => "Gold Ring";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Ring;
    public override ItemRarity Rarity => ItemRarity.Rare;
}