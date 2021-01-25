using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pools
{
    private static DropPool _dropPool;
    public static DropPool DropPool
    {
        get
        {
            if (_dropPool == null)
            {
                _dropPool = new DropPool();
            }

            return _dropPool;
        }
    }
}
