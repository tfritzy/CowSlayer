using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPool : Pool
{
    protected override GameObject CreateObject(int objectType)
    {
        return GameObject.Instantiate(Constants.Prefabs.Drops[(DropType)objectType]);
    }
}
