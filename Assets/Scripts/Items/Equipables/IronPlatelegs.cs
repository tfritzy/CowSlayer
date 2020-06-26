
using System.Collections.Generic;

public class IronPlatelegs : EquipableItem 
{
    public override string Name => "Iron Platelegs";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Legs;
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override List<ItemEffect> EffectPool => new List<ItemEffect>() { new DamageItemEffect(1, 3) };
    protected override int NumEffects => 1;
}