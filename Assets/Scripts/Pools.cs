using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pools
{
    private static GoldPool _goldPool;
    public static GoldPool GoldPool
    {
        get
        {
            if (_goldPool == null)
            {
                _goldPool = new GoldPool();
            }

            return _goldPool;
        }
    }
}
