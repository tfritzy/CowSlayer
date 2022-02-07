using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EquipableItem : Item
{
    public abstract ItemWearLocations.SlotType PlaceWorn { get; }
    public EquipableItem(int level)
    {
        this.Level = level;
    }

    protected static Dictionary<StatModifier, float> GeneratePowerDistributionMap(List<StatModifier> stats, float powerPerAttribute, string id)
    {
        System.Random random = new System.Random(id.GetHashCode());
        Dictionary<StatModifier, float> distributionMap = new Dictionary<StatModifier, float>();
        foreach (StatModifier stat in stats)
        {
            // roll value between 0.5 - 1.5
            distributionMap[stat] = (float)random.NextDouble() + .5f;

            // Be nice and make pretty numbers;
            if (distributionMap[stat] >= 1.45f)
            {
                distributionMap[stat] = 1.5f;
            }

            distributionMap[stat] *= powerPerAttribute;
        }

        return distributionMap;
    }
}
