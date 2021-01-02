
using System.Collections.Generic;

public class GoldRing : EquipableItem
{
    public override string Name => "Gold Ring";
    public override ItemWearLocations.SlotType PlaceWorn => ItemWearLocations.SlotType.Ring;
    public override ItemRarity Rarity => ItemRarity.Rare;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(1, 3);
    protected override List<Effect> SecondaryEffectPool => new List<Effect>() { new DamageItemEffect(1, 3) };
    protected override int NumSecondaryEffects => 0;
}