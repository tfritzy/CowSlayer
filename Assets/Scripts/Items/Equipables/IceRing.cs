
using System.Collections.Generic;

public class IceRing : EquipableItem
{
    public override string Name => "Ice Ring";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Ring;
    public override ItemRarity Rarity => ItemRarity.Legendary;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(5, 8, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => new List<StatModifier>() { };
    protected override int NumSecondaryEffects => 0;
}