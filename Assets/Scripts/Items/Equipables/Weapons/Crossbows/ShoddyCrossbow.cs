using System.Collections.Generic;

public class ShoddyCrossbow : Crossbow
{
    public override string Name => "Shoddy Crossbow";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(2, 4);
    protected override List<Effect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;

}