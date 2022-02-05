
using System.Collections.Generic;

public class GoldRing : EquipableItem
{
    public override string Name => "Gold Ring";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Ring;
    public override ItemRarity Rarity => ItemRarity.Rare;
    public override StatModifier PrimaryAttribute => new FlatDamageStatModifier(1, 3, this.Id);
    protected override List<StatModifier> SecondaryAttributePool => new List<StatModifier>() { new FlatDamageStatModifier(1, 3, this.Id) };
    protected override int NumSecondaryEffects => 0;
}