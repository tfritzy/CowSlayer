using System.Collections.Generic;

public class SteelSword : Sword
{
    public override string Name => "Steel Sword";

    public override ItemRarity Rarity => ItemRarity.Uncommon;

    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(8, 12);

    protected override List<Effect> SecondaryEffectPool => null;

    protected override int NumSecondaryEffects => 0;
}