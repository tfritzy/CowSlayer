using System;
using System.Collections.Generic;

public abstract class Helm : EquipableItem
{
    protected Helm(int level) : base(level)
    {
    }

    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            new ArmorStatModifier(this.Id, this.BasePower * .5f),
        };
    }

    private static Func<string, float, StatModifier>[] secondaryAttributePool = new Func<string, float, StatModifier>[] {
        (string id, float power) => new MaxHealthStatModifier(id, power),
    };

    protected override Func<string, float, StatModifier>[] SecondaryAttributePool => secondaryAttributePool;
}