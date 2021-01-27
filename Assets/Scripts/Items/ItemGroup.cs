using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemGroup
{
    public abstract int MaxSize { get; }
    public abstract string UIPrefabName { get; }
    public string GroupName;
    public virtual bool RequiresReceiveConfirmation => false;
    protected Item[] Items;
    public int numItemsContained;
    private List<GameObject> ButtonInsts;
    private ItemGroup TransferTarget;

    public ItemGroup(string Name)
    {
        this.Items = new Item[MaxSize];
        for (int i = 0; i < this.MaxSize; i++)
        {
            this.Items[i] = null;
        }
        numItemsContained = 0;
        this.GroupName = Name;
    }

    public bool IsFull()
    {
        return numItemsContained == MaxSize;
    }

    public virtual bool CanHoldItem(Item item) => true;

    protected virtual int FindTargetSlot(Item item)
    {
        int firstOpenSlot = -1;
        for (int i = 0; i < MaxSize; i++)
        {
            if (item.Stacks && this.Items[i]?.Name == item.Name)
            {
                firstOpenSlot = i;
                break;
            }

            if (this.Items[i] == null)
            {
                if (firstOpenSlot == -1)
                {
                    firstOpenSlot = i;
                }

                if (item.Stacks == false)
                {
                    break;
                }
            }
        }

        return firstOpenSlot;
    }

    /// <summary>
    /// Finds the slot of the given itemId, or -1 if not found.
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    protected virtual int FindSlotOfItem(string itemId)
    {
        int itemPosition = -1;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i]?.Id == itemId)
            {
                itemPosition = i;
                break;
            }
        }

        return itemPosition;
    }

    public virtual void AddItem(Item item)
    {
        int targetSlot = FindTargetSlot(item);

        if (targetSlot == -1)
        {
            return;
        }

        if (item.Stacks && Items[targetSlot] != null)
        {
            Items[targetSlot].Quantity += item.Quantity;
        }
        else
        {
            Items[targetSlot] = item;
            numItemsContained += 1;
        }

        if (IsMenuOpen())
        {
            SetButtonValues(ButtonInsts[targetSlot], Items[targetSlot]);
        }
    }

    public void AddItems(List<Item> items)
    {
        foreach (Item item in items)
        {
            this.AddItem(item);
        }
    }

    public Item RemoveItem(string itemId, int quantity)
    {
        int itemPosition = FindSlotOfItem(itemId);
        return RemoveItem(itemPosition, quantity);
    }

    public virtual Item RemoveItem(int slotIndex, int quantity)
    {
        Item item = this.Items[slotIndex];

        if (quantity >= item.Quantity)
        {
            this.Items[slotIndex] = null;
            numItemsContained -= 1;
        }
        else
        {
            this.Items[slotIndex].Quantity -= quantity;
        }

        if (IsMenuOpen())
        {
            SetButtonValues(ButtonInsts[slotIndex], this.Items[slotIndex]);
        }

        return item;
    }

    public Item GetItemById(string itemId)
    {
        foreach (Item item in Items)
        {
            if (item?.Id == itemId)
            {
                return item;
            }
        }

        return null;
    }

    public Item GetItemByName(string itemName)
    {
        foreach (Item item in Items)
        {
            if (item?.Name == itemName)
            {
                return item;
            }
        }

        return null;
    }

    /// <summary>
    /// Finds the item of the specified type, if any.
    /// </summary>
    public Item FindItem<T>()
    {
        foreach (Item item in Items)
        {
            if (item is T)
            {
                return item;
            }
        }

        return null;
    }

    public bool TryGetItem<T>(out Item item)
    {
        item = FindItem<T>();
        return item != null;
    }

    public virtual void TransferItemTo(ItemGroup targetItemGroup, string itemId, int quantity, bool hasTransferBeenConfirmed = false)
    {
        if (targetItemGroup.IsFull())
        {
            return;
        }

        Item item = this.GetItemById(itemId);

        if (!targetItemGroup.CanHoldItem(item))
        {
            return;
        }

        int targetSlot = targetItemGroup.FindTargetSlot(item);
        if (targetSlot == -1)
        {
            return;
        }

        if (targetItemGroup.RequiresReceiveConfirmation && hasTransferBeenConfirmed == false)
        {
            targetItemGroup.OpenReceiveConfirmationMenu(item);
            return;
        }

        if (quantity == item.Quantity)
        {
            targetItemGroup.AddItem(RemoveItem(item.Id, quantity));
        }
        else
        {
            Item newItem = item.SplitStack(quantity);

            if (IsMenuOpen())
            {
                SetButtonValues(ButtonInsts[this.FindSlotOfItem(item.Id)], item);
            }

            targetItemGroup.AddItem(newItem);
        }
    }

    public bool IsMenuOpen()
    {
        return chestUI != null;
    }

    private GameObject chestUI;
    /// <summary>
    /// Opens the item ui at the given position
    /// 0 is bottom of screen and 1 is top.
    /// </summary>
    /// <param name="menuHeight">0 is bottom, 1 is top</param>
    /// <param name="transferTarget"></param>
    public Transform OpenMenu(float menuHeight, ItemGroup transferTarget = null)
    {
        Vector3 uiPosition = new Vector2(
            Constants.Persistant.InteractableCanvas.GetComponent<RectTransform>().rect.width * .5f,
            Constants.Persistant.InteractableCanvas.GetComponent<RectTransform>().rect.height * menuHeight
        );
        chestUI = GameObject.Instantiate(Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/{UIPrefabName}"),
            uiPosition, new Quaternion(), Constants.Persistant.InteractableUI.transform);
        GameObject backdrop = chestUI.transform.Find("Backdrop").gameObject;
        Transform background = backdrop.transform.Find("Background");
        background.Find("Outline").GetComponent<Image>().color = Constants.UI.Colors.BrightBase;
        background.GetComponent<Image>().color = Constants.UI.Colors.Base;
        this.TransferTarget = transferTarget;
        ButtonInsts = new List<GameObject>();
        for (int i = 0; i < MaxSize; i++)
        {
            GameObject inst = backdrop.transform.Find($"Slot{i}").gameObject;
            ButtonInsts.Add(inst);
            SetButtonValues(inst, this.Items[i]);
        }
        GameObject.Instantiate(Constants.Prefabs.CloseMenuButton, Constants.Persistant.InteractableUI);
        backdrop.transform.Find("GroupName").GetComponent<Text>().text = GroupName;
        return chestUI.transform;
    }

    private void SetButtonValues(GameObject button, Item item)
    {
        if (item == null)
        {
            button.transform.Find("Background").GetComponent<Image>().color = Constants.UI.Colors.LightBase;
            button.transform.Find("Icon").GetComponent<Image>().color = Color.clear;
            button.transform.Find("Outline").GetComponent<Image>().color = Constants.UI.Colors.BrightBase;
            button.transform.Find("Quantity").GetComponent<Text>().text = string.Empty;
            button.GetComponent<ChestButton>().SetItem(null);
        }
        else
        {
            button.transform.Find("Icon").GetComponent<Image>().sprite = item.GetIcon();
            button.transform.Find("Icon").GetComponent<Image>().color = Color.white;
            button.transform.Find("Background").GetComponent<Image>().color = item.GetDarkRarityColor();
            button.transform.Find("Outline").GetComponent<Image>().color = item.GetRarityColor();
            button.transform.Find("Quantity").GetComponent<Text>().text = item.Quantity > 1 ? item.Quantity.ToString() : string.Empty;
            button.GetComponent<ChestButton>().SourceItemGroup = this;
            button.GetComponent<ChestButton>().SetItem(item);
        }


        button.GetComponent<ChestButton>().TargetItemGroup = this.TransferTarget;
    }

    public virtual void OpenReceiveConfirmationMenu(Item item)
    {
        throw new System.NotImplementedException();
    }
}
