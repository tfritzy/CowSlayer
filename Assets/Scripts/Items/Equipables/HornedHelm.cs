
using System.Collections.Generic;

public class HornedHelm : EquipableItem 
{
    public override string Name => "Horned Helm";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Head;
    public override ItemRarity Rarity => ItemRarity.Uncommon;
    protected override ItemEffect PrimaryEffectPrefab => new DamageItemEffect(1, 5);
    protected override List<ItemEffect> SecondaryEffectPool => new List<ItemEffect>() { };
    protected override int NumSecondaryEffects => 0;
}