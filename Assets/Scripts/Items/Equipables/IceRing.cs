
using System.Collections.Generic;

public class IceRing : EquipableItem 
{
    public override string Name => "Ice Ring";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Ring;
    public override ItemRarity Rarity => ItemRarity.Legendary;
    protected override List<ItemEffect> EffectPool => new List<ItemEffect>() { new DamageItemEffect(5, 8) };
    protected override int NumEffects => 1;
}