using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WornItemsGroup : ItemGroup
{
    protected Character Bearer;

    public WornItemsGroup(string Name, Character bearer) : base(Name)
    {
        this.Bearer = bearer;
        this.fists = new Fists();
    }

    public override int MaxSize => 9;
    public override string UIPrefabName => "PlayerWornItems";
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
        ((EquipableItem)item).OnUnequip(Bearer);
        return item;
    }

    public void ApplyItemEffects(Character character)
    {
        foreach (Item item in Items)
        {
            item?.ApplyEffects(character);
        }
    }
}