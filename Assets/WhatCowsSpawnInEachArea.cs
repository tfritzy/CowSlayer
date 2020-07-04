using System.Collections.Generic;

public static class WhatCowsSpawnInEachArea
{
    public static readonly Dictionary<Area, List<List<CowType>>> Spawns = new Dictionary<Area, List<List<CowType>>>()
    {
        {
            Area.Grasslands,
            new List<List<CowType>>()
            {
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow },
                new List<CowType>() { CowType.WimpyCow }
            }
        }
    };

    public static readonly Dictionary<Area, List<CowType>> ZoneGuardians = new Dictionary<Area, List<CowType>>()
    {
        {
            Area.Grasslands,
            new List<CowType>()
            {
                CowType.WimpyCow,
                CowType.WimpyCow,
                CowType.WimpyCow,
                CowType.WimpyCow,
                CowType.WimpyCow,
                CowType.WimpyCow,
                CowType.WimpyCow,
                CowType.WimpyCow,
            }
        }
    };
}
