using System;
using System.Collections.Generic;

public class Arrow : Item
{
    public override string Name => "Arrow";
    public override ItemRarity Rarity => ItemRarity.Common;
    protected override Effect PrimaryEffectPrefab => new DamageItemEffect(1, 1);
    protected override List<Effect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;
    public override bool Stacks => true;
    protected override List<Tuple<int, string>> Icons
    {
        get
        {
            return new List<Tuple<int, string>>
            {
                new Tuple<int, string>(1, "Arrow_1"),
                new Tuple<int, string>(2, "Arrow_2"),
                new Tuple<int, string>(3, "Arrow_3"),
                new Tuple<int, string>(5, "Arrow_5"),
            };
        }
    }
}