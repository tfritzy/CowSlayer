using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPool : Pool
{
    protected override GameObject CreateObject(int objectType)
    {
        return GameObject.Instantiate(Constants.Prefabs.Coins[objectType]);
    }
}
