using UnityEngine;
using System.Collections;

public class ChestItemGroup : ItemGroup
{
    public override int MaxSize => 20;
    public override string UIPrefabName => "ChestUI";

    public ChestItemGroup(string Name) : base(Name)
    {
    }
}
