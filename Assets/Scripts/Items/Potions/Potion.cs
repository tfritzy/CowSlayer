
using System.Collections.Generic;

public abstract class Potion : Item
{
    protected override List<StatModifier> SecondaryAttributePool => null;
    protected override int NumSecondaryEffects => 0;
}