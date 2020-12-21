
using System.Collections.Generic;

public abstract class Potion : Item
{
    protected override List<ItemEffect> SecondaryEffectPool => null;
    protected override int NumSecondaryEffects => 0;
}