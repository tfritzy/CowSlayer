﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemGroup
{
    public abstract int MaxSize { get; }
    public abstract string UIPrefabName { get; }
    public string GroupName;
    public virtual bool RequiresRecieveConfirmation => false;
    private GameObject emptyItemSlot;
    protected Item[] Items;
    public int numItemsContained;
    private List<GameObject> ButtonInsts;
    private ItemGroup TransferTarget;

    public ItemGroup(string Name)
    {
        emptyItemSlot = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/EmptyItemSlot");
        this.Items = new Item[MaxSize];
        for(int i = 0; i < this.MaxSize; i++)
        {
            this.Items[i] = null;
        }
        numItemsContained = 0;
        this.GroupName = Name;
    }

    public bool IsFull(){
        return numItemsContained == MaxSize;
    }

    protected virtual int FindTargetSlot(Item item)
    {
        int firstOpenSlot = 0;
        for (int i = 0; i < MaxSize; i++){
            if(this.Items[i] == null){
                firstOpenSlot = i;
                break;
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
        if (Items[targetSlot] != null)
        {
            return;
        }

        Items[targetSlot] = item;
        numItemsContained += 1;
        if (IsMenuOpen())
        {
            SetButtonValues(ButtonInsts[targetSlot], item);
        }
    }

    public void AddItems(List<Item> items)
    {
        foreach (Item item in items)
        {
            this.AddItem(item);
        }
    }

    public Item RemoveItem(string itemId)
    {
        int itemPosition = FindSlotOfItem(itemId);
        return RemoveItem(itemPosition);
    }

    public virtual Item RemoveItem(int slotIndex)
    {
        Item item = this.Items[slotIndex];
        this.Items[slotIndex] = null;
        numItemsContained -= 1;
        if (IsMenuOpen())
        {
            SetButtonValues(ButtonInsts[slotIndex], null);
        }
        return item;
    }

    public Item GetItem(string itemId)
    {
        foreach (Item item in Items){
            if (item?.Id == itemId){
                return item;
            }
        }
        return null;
    }

    public virtual void TransferItem(ItemGroup targetItemGroup, string itemId, bool hasTransferBeenConfirmed = false)
    {
        if (targetItemGroup.IsFull()){
            return;
        }

        Item item = this.GetItem(itemId);
        int targetSlot = targetItemGroup.FindTargetSlot(item);
        if (targetItemGroup.Items[targetSlot] != null)
        {
            return;
        }

        if (targetItemGroup.RequiresRecieveConfirmation && hasTransferBeenConfirmed == false)
        {
            targetItemGroup.OpenRecieveConfirmationMenu(item);
            return;
        }

        targetItemGroup.AddItem(RemoveItem(itemId));
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
            Constants.GameObjects.InteractableCanvas.GetComponent<RectTransform>().rect.width * .5f,
            Constants.GameObjects.InteractableCanvas.GetComponent<RectTransform>().rect.height * menuHeight
        );
        chestUI = GameObject.Instantiate(Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/{UIPrefabName}"), 
            uiPosition, new Quaternion(), Constants.GameObjects.InteractableUI.transform);
        GameObject backdrop = chestUI.transform.Find("Backdrop").gameObject;
        this.TransferTarget = transferTarget;
        ButtonInsts = new List<GameObject>();
        for (int i = 0; i < MaxSize; i++)
        {
            GameObject inst = backdrop.transform.Find($"Slot{i}").gameObject;
            ButtonInsts.Add(inst);
            SetButtonValues(inst, this.Items[i]);
        }
        GameObject.Instantiate(Constants.Prefabs.CloseMenuButton, Constants.GameObjects.InteractableUI);
        backdrop.transform.Find("GroupName").GetComponent<Text>().text = GroupName;
        return chestUI.transform;
    }

    private static Sprite _squareIconBackground;
    protected static Sprite SquareIconBackground
    {
        get
        {
            if (_squareIconBackground == null)
            {
                _squareIconBackground = Resources.Load<Sprite>($"{Constants.FilePaths.Icons}/SquareItemIcon");
            }
            return _squareIconBackground;
        }
    }

    private void SetButtonValues(GameObject button, Item item)
    {
        if (item == null)
        {
            button.GetComponent<Image>().sprite = emptyItemSlot.GetComponent<Image>().sprite;
            button.GetComponent<Image>().color = Color.white;
            button.transform.Find("Icon").GetComponent<Image>().color = Color.clear;
            button.GetComponent<ChestButton>().ItemId = null;
        } else
        {
            button.GetComponent<Image>().sprite = SquareIconBackground;
            button.GetComponent<Image>().color = item.GetRarityColor();
            button.transform.Find("Icon").GetComponent<Image>().sprite = item.GetIcon();
            button.transform.Find("Icon").GetComponent<Image>().color = Color.white;
            button.GetComponent<ChestButton>().SourceItemGroup = this;
            button.GetComponent<ChestButton>().ItemId = item.Id;
        }
        button.GetComponent<ChestButton>().TargetItemGroup = this.TransferTarget;
    }

    public virtual void OpenRecieveConfirmationMenu(Item item)
    {
        throw new System.NotImplementedException();
    }
}
