using System.Collections.Generic;

public static class WhatCowsSpawnInEachArea
{
    public static readonly Dictionary<Area, List<List<CowType>>> Spawns = new Dictionary<Area, List<List<CowType>>>()
    {
        {
            Area.Grasslands,
            new List<List<CowType>>()
            {
                new List<CowType>() { CowType.SpearThrower },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow },
                new List<CowType>() { CowType.BasicCow }
            }
        }
    };

    public static readonly Dictionary<Area, List<CowType>> ZoneGuardians = new Dictionary<Area, List<CowType>>()
    {
        {
            Area.Grasslands,
            new List<CowType>()
            {
                CowType.SpearThrower,
                CowType.BasicCow,
                CowType.BasicCow,
                CowType.BasicCow,
                CowType.BasicCow,
                CowType.BasicCow,
                CowType.BasicCow,
                CowType.BasicCow,
            }
        }
    };
}
