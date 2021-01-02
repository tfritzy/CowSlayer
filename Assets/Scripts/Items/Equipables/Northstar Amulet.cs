
using System.Collections.Generic;

public class NorthstarAmulet : EquipableItem
{
    public override string Name => "Northstar Amulet";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Amulet;
    public override ItemRarity Rarity => ItemRarity.Exquisite;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(5, 8);
    protected override List<Effect> SecondaryEffectPool => new List<Effect>() { new DamageItemEffect(3, 5), new DamageItemEffect(3, 5) };
    protected override int NumSecondaryEffects => 2;
}