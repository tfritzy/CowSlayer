
using System.Collections.Generic;

public class IronPlatelegs : EquipableItem
{
    public override string Name => "Iron Platelegs";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Legs;
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(1, 3);
    protected override List<Effect> SecondaryEffectPool => new List<Effect>() { };
    protected override int NumSecondaryEffects => 0;
}