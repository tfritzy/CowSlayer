
using System.Collections.Generic;

public class SmallHealthPotion : Potion
{
    public override string Name => "Small Health Potion";
    protected override ItemEffect PrimaryEffectPrefab => new SmallHealthRestore();
    protected override List<ItemEffect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;
    public override bool IsInfiniteInShop => true;
}