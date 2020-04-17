using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornItemsGroup : ItemGroup
{
    public override int MaxSize => 9;
    public override string UIPrefabName => "PlayerWornItems";
    
    protected override int FindTargetSlot(Item item)
    {
        return (int)((EquipableItem)item).PlaceWorn;
    }
}