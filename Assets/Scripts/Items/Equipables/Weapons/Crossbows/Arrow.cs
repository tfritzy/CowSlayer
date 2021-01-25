using System.Collections.Generic;

public class Arrow : Item
{
    public override string Name => "Arrow";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(1, 1);
    protected override List<Effect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;
    public override bool Stacks => true;
}