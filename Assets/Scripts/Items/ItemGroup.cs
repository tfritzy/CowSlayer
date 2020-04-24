using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemGroup
{
    public abstract int MaxSize { get; }
    public abstract string UIPrefabName { get; }

    private GameObject emptyItemSlot;
    protected Item[] Items;
    private List<GameObject> ButtonInsts;
    private ItemGroup TransferTarget;

    public ItemGroup()
    {
        emptyItemSlot = Resources.Load<GameObject>($"{Constants.FilePaths.UIPrefabs}/EmptyItemSlot");
        this.Items = new Item[MaxSize];
        for(int i = 0; i < this.MaxSize; i++)
        {
            this.Items[i] = null;
        }
    }

    public bool IsFull(){
        return Items.Length == MaxSize;
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

    public void AddItem(Item item)
    {
        int targetSlot = FindTargetSlot(item);
        Items[targetSlot] = item;
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
        int itemPosition = 0;
        for (int i = 0; i < Items.Length; i++){
            if (Items[i]?.Id == itemId){
                itemPosition = i;
                break;
            }
        }
        Item item = Items[itemPosition];
        Items[itemPosition] = null;
        if (IsMenuOpen())
        {
            SetButtonValues(ButtonInsts[itemPosition], null);
        }
        return item;
    }

    public Item GetItem(string itemId)
    {
        foreach (Item item in Items){
            if (item.Id == itemId){
                return item;
            }
        }
        return null;
    }

    public virtual void TransferItem(ItemGroup targetItemGroup, string itemId)
    {
        if (this.IsFull()){
            return;
        }
        targetItemGroup.AddItem(RemoveItem(itemId));
    }

    public bool IsMenuOpen()
    {
        return chestUI != null;
    }

    private GameObject chestUI;
    public void OpenMenu(float menuHeight, ItemGroup transferTarget = null)
    {
        Vector3 uiPosition = new Vector2(
            Constants.GameObjects.InteractableCanvas.GetComponent<RectTransform>().rect.width * .5f,
            Constants.GameObjects.InteractableCanvas.GetComponent<RectTransform>().rect.height * menuHeight
        );
        chestUI = GameObject.Instantiate(Resources.Load<GameObject>($"{Constants.FilePaths.UIPrefabs}/{UIPrefabName}"), 
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
    }

    private void SetButtonValues(GameObject button, Item item)
    {
        if (item == null)
        {
            button.GetComponent<Image>().sprite = emptyItemSlot.GetComponent<Image>().sprite;
            button.GetComponent<ChestButton>().ItemId = null;
        } else
        {
            button.GetComponent<Image>().sprite = item.GetIcon();
            button.GetComponent<ChestButton>().SourceItemGroup = this;
            button.GetComponent<ChestButton>().ItemId = item.Id;
        }
        button.GetComponent<ChestButton>().TargetItemGroup = this.TransferTarget;
    }
}
