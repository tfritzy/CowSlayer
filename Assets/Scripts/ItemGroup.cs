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
    private Dictionary<string, Item> Items;
    private List<string> ItemIds;
    private List<GameObject> ButtonInsts;
    private ItemGroup TransferTarget;

    public ItemGroup(int maxSize, Vector2 dimensions, string UIPrefabName)
    {
        this.MaxSize = maxSize;
        this.dimensions = dimensions;
        this.UIPrefabName = UIPrefabName;
        emptyItemSlot = Resources.Load<GameObject>($"{Constants.FilePaths.UIPrefabs}/EmptyItemSlot");
        this.Items = new Dictionary<string, Item>();
        this.ItemIds = new List<string>();
    }

    public void AddItem(Item item)
    {
        this.Items.Add(item.Id, item);
        this.ItemIds.Add(item.Id);
        if (IsMenuOpen())
        {
            SetButtonValues(ButtonInsts[Items.Count - 1], item);
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
        Item item = Items[itemId];
        ItemIds.Remove(itemId);
        Items.Remove(itemId);
        if (IsMenuOpen())
        {
            SetButtonValues(ButtonInsts[Items.Count - 1], null);
        }
        return item;
    }

    public Item GetItem(string itemId)
    {
        return Items[itemId];
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
        int width = (int)backdrop.GetComponent<RectTransform>().rect.width;
        int height = (int)backdrop.GetComponent<RectTransform>().rect.height;
        int xDistBetweenIcons = (int)(width / dimensions.x);
        int yDistBetweenIcons = (int)(height / dimensions.y);
        int itemCount = 0;
        List<string> itemKeys = new List<string>(this.Items.Keys);
        this.ButtonInsts = new List<GameObject>();
        for (int j = height / 2 - yDistBetweenIcons / 2; j > -height / 2; j -= yDistBetweenIcons)
        {
            for (int i = -width / 2 + xDistBetweenIcons / 2; i < width / 2; i += xDistBetweenIcons)
            {
                GameObject inst = GameObject.Instantiate(emptyItemSlot, backdrop.transform.position + new Vector3(i, j),
                    new Quaternion(), backdrop.transform);
                ButtonInsts.Add(inst);
                if (itemCount < Items.Count)
                {
                    SetButtonValues(inst, this.Items[ItemIds[itemCount]], transferTarget);
                } else
                {
                    SetButtonValues(inst, null, transferTarget);
                }
                itemCount += 1;
            }
        }
    }

    private void SetButtonValues(GameObject button, Item item, ItemGroup transferTarget = null)
    {
        if (item == null)
        {
            button.GetComponent<Image>().sprite = emptyItemSlot.GetComponent<Image>().sprite;
        } else
        {
            button.GetComponent<Image>().sprite = item.GetIcon();
            button.GetComponent<ChestButton>().SourceItemGroup = this;
            button.GetComponent<ChestButton>().TargetItemGroup = transferTarget;
            button.GetComponent<ChestButton>().ItemId = item.Id;
        }
    }
}
