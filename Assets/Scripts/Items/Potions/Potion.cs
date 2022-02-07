
using System;
using System.Collections.Generic;

public abstract class Potion : Item
{
    private static Func<string, float, StatModifier>[] secondaryAttributePool = new Func<string, float, StatModifier>[] { };
    protected override Func<string, float, StatModifier>[] SecondaryAttributePool => secondaryAttributePool;
}