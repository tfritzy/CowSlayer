using System.Collections.Generic;

public class WoodenSword : Sword
{
    public override string Name => "Wooden Sword";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(1, 5);
    protected override List<Effect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;
}