
using System.Collections.Generic;

public class HornedHelm : EquipableItem
{
    public override string Name => "Horned Helm";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Head;
    public override ItemRarity Rarity => ItemRarity.Uncommon;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(1, 5, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => new List<StatModifier>() { };
    protected override int NumSecondaryEffects => 0;
}