
using System.Collections.Generic;

public class LeatherBody : EquipableItem
{
    public override string Name => "Leather Body";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Chest;
    public override ItemRarity Rarity => ItemRarity.Common;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(1, 3, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => new List<StatModifier>() { };
    protected override int NumSecondaryEffects => 0;
}