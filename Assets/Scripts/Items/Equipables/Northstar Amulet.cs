
using System.Collections.Generic;

public class NorthstarAmulet : EquipableItem
{
    public override string Name => "Northstar Amulet";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Amulet;
    public override ItemRarity Rarity => ItemRarity.Exquisite;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(5, 8, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => new List<StatModifier>() { new FlatDamageStatModifier(3, 5, this.Id), new FlatDamageStatModifier(3, 5, this.Id) };
    protected override int NumSecondaryEffects => 2;
}