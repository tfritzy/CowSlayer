using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPool : Pool
{
    protected override GameObject CreateObject(int objectType)
    {
        switch (objectType)
        {
            case (1):
                return GameObject.Instantiate(Constants.Prefabs.Gold.GoldCoin);
            default:
                throw new System.ArgumentException($"No implementation for gold type of {objectType}");
        }
    }
}
