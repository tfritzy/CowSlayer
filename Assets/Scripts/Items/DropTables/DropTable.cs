using System.Collections.Generic;
using UnityEngine;

public abstract class DropTable
{
    public Dictionary<Drop, int> DropChances;

    public DropTable()
    {
        SetValues();
    }

    public virtual void SetValues()
    {
        int totalChance = 0;
        foreach (int chance in DropChances.Values)
        {
            totalChance += chance;
        }

        if (totalChance > 100)
        {
            throw new System.ArgumentException("Total chances in drop table are over 100");
        }
    }

    public Drop RollDrop()
    {
        float roll = Random.Range(0, 100);
        foreach (Drop drop in DropChances.Keys)
        {
            roll -= DropChances[drop];
            if (roll <= 0)
            {
                return drop;
            }
        }

        return null;
    }
}