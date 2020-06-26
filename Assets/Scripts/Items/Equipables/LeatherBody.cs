
using System.Collections.Generic;

public class LeatherBody : EquipableItem 
{
    public override string Name => "Leather Body";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Chest;
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override List<ItemEffect> EffectPool => new List<ItemEffect>() { new DamageItemEffect(1, 3) };
    protected override int NumEffects => 1;
}