using System;
using System.Collections.Generic;

public abstract class Legging : EquipableItem
{
    protected Legging(int level) : base(level)
    {
    }

    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            new ArmorStatModifier(this.Id, this.BasePower * .75f),
        };
    }

    private static Func<string, float, StatModifier>[] secondaryAttributePool = new Func<string, float, StatModifier>[] {
        (string id, float power) => new MaxHealthStatModifier(id, power),
        (string id, float power) => new MovementSpeedStatModifier(id, power),
    };

    protected override Func<string, float, StatModifier>[] SecondaryAttributePool => secondaryAttributePool;
}