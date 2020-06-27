
using System.Collections.Generic;

public class LeatherBody : EquipableItem 
{
    public override string Name => "Leather Body";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Chest;
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override ItemEffect PrimaryEffectPrefab => new DamageItemEffect(1, 3);
    protected override List<ItemEffect> SecondaryEffectPool => new List<ItemEffect>() {  };
    protected override int NumSecondaryEffects => 0;
}