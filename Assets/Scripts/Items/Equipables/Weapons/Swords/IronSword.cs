using System.Collections.Generic;

public class IronSword : Sword
{
    public override string Name => "Iron Sword";
    public override ItemRarity Rarity => ItemRarity.Uncommon;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(3, 8);
    protected override List<Effect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;
}