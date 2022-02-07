using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornItemsGroup : ItemGroup
{
    protected Character Bearer;

    public WornItemsGroup(string Name, Character bearer) : base(Name)
    {
        this.Bearer = bearer;
    }


    private int maxSize = 9;
    public override int MaxSize => maxSize;

    private string uIPrefabName = "PlayerWornItems";
    public override string UIPrefabName => uIPrefabName;
    private Fists fists;
    public override bool CanHoldItem(Item item)
    {
        return item is EquipableItem;
    }

    public Weapon Weapon
    {
        get
        {
            if (Items[ItemWearLocations.Slots[ItemWearLocations.SlotType.MainHand][0]] == null)
            {
                if (this.fists == null)
                {
                    this.fists = new Fists(1);
                    this.fists.OnEquip(Bearer);
                }

                return this.fists;
            }

            return (Weapon)Items[ItemWearLocations.Slots[ItemWearLocations.SlotType.MainHand][0]];
        }
        set
        {
            Items[ItemWearLocations.Slots[ItemWearLocations.SlotType.MainHand][0]] = value;
        }
    }


    protected override int FindTargetSlot(Item item)
    {
        ItemWearLocations.SlotType slot = ((EquipableItem)item).PlaceWorn;
        int index = 0;
        while (Items[ItemWearLocations.Slots[slot][index]] != null)
        {
            if (index == ItemWearLocations.Slots[slot].Length - 1)
            {
                return -1;
            }

            index += 1;
        }

        return ItemWearLocations.Slots[slot][index];
    }

    public override void TransferItemTo(ItemGroup targetItemGroup, string itemId, int quantity, bool hasTransferBeenConfirmed = false)
    {
        targetItemGroup.AddItem(RemoveItem(itemId, quantity));
    }

    public override void AddItem(Item item)
    {
        base.AddItem(item);
        ((EquipableItem)item).OnEquip(Bearer);
    }

    public override Item RemoveItem(int slotIndex, int quantity)
    {
        Item item = base.RemoveItem(slotIndex, quantity);
        ((EquipableItem)item).OnUnEquip(Bearer);
        return item;
    }
}