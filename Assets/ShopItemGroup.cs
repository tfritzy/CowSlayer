using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemGroup : ItemGroup
{
    private HashSet<string> PurchasedItems;
    private GameObject PurchaseItemMenu;
    public override int MaxSize => 20;
    public override string UIPrefabName => "ChestUI";
    public override bool RequiresRecieveConfirmation => true;

    private GameObject currentlyOpenConfirmationWindow;

    public ShopItemGroup(string Name) : base(Name)
    {
        PurchasedItems = new HashSet<string>();
    }

    public override void TransferItem(ItemGroup targetItemGroup, string itemId, bool hasTransferBeenConfirmed = false)
    {
        if (PurchasedItems.Contains(itemId))
        {
            base.TransferItem(targetItemGroup, itemId);
            PurchasedItems.Remove(itemId);
        }
        else
        {
            OpenPurchaseItemMenu(itemId);
        }
    }

    protected void OpenPurchaseItemMenu(string itemId)
    {
        Item item = GetItem(itemId);
        if (currentlyOpenConfirmationWindow != null)
        {
            GameObject.Destroy(currentlyOpenConfirmationWindow);
        }

        currentlyOpenConfirmationWindow = GameObject.Instantiate(Constants.Prefabs.PurchaseItemMenu, Constants.GameObjects.InteractableUI);
        currentlyOpenConfirmationWindow.GetComponent<PurchaseItemMenu>().Initialize(this, item);

    }

    public void PurchaseItem(Item item)
    {
        if (Constants.GameObjects.PlayerScript.Gold >= item.Price)
        {
            Constants.GameObjects.PlayerScript.Gold -= item.Price;
            PurchasedItems.Add(item.Id);
            TransferItem(Constants.GameObjects.PlayerScript.Inventory, item.Id);
        }
    }

    public void SellItem(Item item)
    {
        Constants.GameObjects.PlayerScript.Gold += item.Price;
        Constants.GameObjects.PlayerScript.Inventory.TransferItem(this, item.Id, hasTransferBeenConfirmed: true);
    }

    public override void OpenRecieveConfirmationMenu(Item item)
    {
        if (currentlyOpenConfirmationWindow != null)
        {
            GameObject.Destroy(currentlyOpenConfirmationWindow);
        }
        currentlyOpenConfirmationWindow = GameObject.Instantiate(Constants.Prefabs.PurchaseItemMenu, Constants.GameObjects.InteractableUI);
        currentlyOpenConfirmationWindow.GetComponent<PurchaseItemMenu>().Initialize(this, item, buyMode: false);
    }
}
