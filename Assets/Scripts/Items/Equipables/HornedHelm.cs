
using System.Collections.Generic;

public class HornedHelm : EquipableItem 
{
    public override string Name => "Horned Helm";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Head;
    public override ItemRarity Rarity => ItemRarity.Uncommon;
    protected override List<ItemEffect> EffectPool => new List<ItemEffect>() { new DamageItemEffect(1, 3) };
    protected override int NumEffects => 1;
}