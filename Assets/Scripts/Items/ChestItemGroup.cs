using UnityEngine;
using System.Collections;

public class ChestItemGroup : ItemGroup
{

    private int maxSize = 20;
    public override int MaxSize => maxSize;

    private string uIPrefabName = "ChestUI";
    public override string UIPrefabName => uIPrefabName;

    public ChestItemGroup(string Name) : base(Name)
    {
    }
}
