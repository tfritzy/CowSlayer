using System.Collections.Generic;

public static class WhatCowsSpawnInEachArea
{
    public static readonly Dictionary<Area, List<List<CowType>>> Spawns = new Dictionary<Area, List<List<CowType>>>()
    {
        {
            Area.Desert,
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
}
