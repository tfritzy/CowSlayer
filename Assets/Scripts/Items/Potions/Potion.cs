
using System.Collections.Generic;

public abstract class Potion : Item
{
    public override bool IsInfiniteInShop => true;
    protected override List<ItemEffect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;
}