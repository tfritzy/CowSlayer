using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGroup
{
    public int MaxSize;
    public Vector2 dimensions;
    public string UIPrefabName;

    private GameObject emptyItemSlot;
    private Item[] Items;
    private List<GameObject> ButtonInsts;
    private ItemGroup TransferTarget;

    public ItemGroup(int maxSize, Vector2 dimensions, string UIPrefabName)
    {
        this.MaxSize = maxSize;
        this.dimensions = dimensions;
        this.UIPrefabName = UIPrefabName;
        emptyItemSlot = Resources.Load<GameObject>($"{Constants.FilePaths.UIPrefabs}/EmptyItemSlot");
        this.Items = new Item[MaxSize];
        for(int i = 0; i < this.MaxSize; i++){
            this.Items[i] = null;
        }
    }

    public void AddItem(Item item)
    {
        int firstOpenSlot = 0;
        for (int i = 0; i < MaxSize; i++){
            if(this.Items[i] == null){
                firstOpenSlot = i;
                break;
            }
        }
        Items[firstOpenSlot] = item;
        if (IsMenuOpen())
        {
            SetButtonValues(ButtonInsts[firstOpenSlot], item);
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

    public void TransferItem(ItemGroup targetItemGroup, string itemId)
    {
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
        int width = (int)backdrop.GetComponent<RectTransform>().rect.width;
        int height = (int)backdrop.GetComponent<RectTransform>().rect.height;
        int xDistBetweenIcons = (int)(width / dimensions.x);
        int yDistBetweenIcons = (int)(height / dimensions.y);
        int itemCount = 0;
        this.ButtonInsts = new List<GameObject>();
        for (int j = height / 2 - yDistBetweenIcons / 2; j > -height / 2; j -= yDistBetweenIcons)
        {
            for (int i = -width / 2 + xDistBetweenIcons / 2; i < width / 2; i += xDistBetweenIcons)
            {
                GameObject inst = GameObject.Instantiate(emptyItemSlot, backdrop.transform.position + new Vector3(i, j),
                    new Quaternion(), backdrop.transform);
                ButtonInsts.Add(inst);
                if (itemCount < Items.Length)
                {
                    SetButtonValues(inst, this.Items[itemCount]);
                } else
                {
                    SetButtonValues(inst, null);
                }
                itemCount += 1;
            }
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
