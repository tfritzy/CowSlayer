using System;
using System.Collections.Generic;

public abstract class Ring : EquipableItem
{
    protected Ring(int level) : base(level)
    {
    }

    protected override List<StatModifier> GeneratePrimaryAttributes()
    {
        return new List<StatModifier>() {
            // Rings aren't for defense, so only 10% of power goes to armor.
            new ArmorStatModifier(this.Id, this.BasePower * .1f),
        };
    }

    private static Func<string, float, StatModifier>[] secondaryAttributePool = new Func<string, float, StatModifier>[] {
        (string id, float power) => new FlatDamageStatModifier(id, power),
        (string id, float power) => new MaxHealthStatModifier(id, power),
        (string id, float power) => new MovementSpeedStatModifier(id, power),
    };

    protected override Func<string, float, StatModifier>[] SecondaryAttributePool => secondaryAttributePool;
}