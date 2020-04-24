using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornItemsGroup : ItemGroup
{
    public override int MaxSize => 9;
    public override string UIPrefabName => "PlayerWornItems";

    protected override int FindTargetSlot(Item item)
    {
        ItemWearLocations.SlotType slot = ((EquipableItem)item).PlaceWorn;
        int index = 0;
        while(Items[ItemWearLocations.Slots[slot][index]] != null)
        {
            if (index == ItemWearLocations.Slots[slot].Length - 1)
            {
                break;
            }
            index += 1;
        }
        return ItemWearLocations.Slots[slot][index];
    }

    public override void TransferItem(ItemGroup targetItemGroup, string itemId)
    {
         
        targetItemGroup.AddItem(RemoveItem(itemId));
    }
}