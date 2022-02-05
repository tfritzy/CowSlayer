
using System.Collections.Generic;

public class IronPlatelegs : EquipableItem
{
    public override string Name => "Iron Platelegs";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Legs;
    public override ItemRarity Rarity => ItemRarity.Common;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(1, 3, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => new List<StatModifier>() { };
    protected override int NumSecondaryEffects => 0;
}