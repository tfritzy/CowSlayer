using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornItemsGroup : ItemGroup
{
    public override int MaxSize => 9;
    public override string UIPrefabName => "PlayerWornItems";

    public WornItemsGroup()
    {
    }
}