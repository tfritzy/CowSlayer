
using System.Collections.Generic;

public class IceRing : EquipableItem
{
    public override string Name => "Ice Ring";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Ring;
    public override ItemRarity Rarity => ItemRarity.Legendary;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(5, 8);
    protected override List<Effect> SecondaryEffectPool => new List<Effect>() { };
    protected override int NumSecondaryEffects => 0;
}