
using System.Collections.Generic;

public class NorthstarAmulet : EquipableItem 
{
    public override string Name => "Northstar Amulet";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Amulet;
    public override ItemRarity Rarity => ItemRarity.Exquisite;
    protected override List<ItemEffect> EffectPool => new List<ItemEffect>() { new DamageItemEffect(1, 3) };
    protected override int NumEffects => 1;
}