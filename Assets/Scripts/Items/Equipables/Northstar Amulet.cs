
using System.Collections.Generic;

public class NorthstarAmulet : EquipableItem 
{
    public override string Name => "Northstar Amulet";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Amulet;
    public override ItemRarity Rarity => ItemRarity.Exquisite;
    protected override ItemEffect PrimaryEffectPrefab => new DamageItemEffect(5, 8);
    protected override List<ItemEffect> SecondaryEffectPool => new List<ItemEffect>() { new DamageItemEffect(3, 5), new DamageItemEffect(3, 5) };
    protected override int NumSecondaryEffects => 2;
}