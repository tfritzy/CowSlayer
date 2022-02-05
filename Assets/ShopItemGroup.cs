using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemGroup : ItemGroup
{
    private HashSet<string> PurchasedItems;

    private int maxSize = 20;
    public override int MaxSize => maxSize;

    private string uIPrefabName = "ChestUI";
    public override string UIPrefabName => uIPrefabName;

    private bool requiresReceiveConfirmation = true;
    public override bool RequiresReceiveConfirmation => requiresReceiveConfirmation;

    private GameObject currentlyOpenConfirmationWindow;

    public ShopItemGroup(string Name) : base(Name)
    {
        PurchasedItems = new HashSet<string>();
    }

    public override void TransferItemTo(ItemGroup targetItemGroup, string itemId, int quantity, bool hasTransferBeenConfirmed = false)
    {
        if (PurchasedItems.Contains(itemId))
        {
            base.TransferItemTo(targetItemGroup, itemId, quantity);
            PurchasedItems.Remove(itemId);
        }
        else
        {
            OpenPurchaseItemMenu(itemId);
        }
    }

    protected void OpenPurchaseItemMenu(string itemId)
    {
        Item item = GetItemById(itemId);
        if (currentlyOpenConfirmationWindow != null)
        {
            GameObject.Destroy(currentlyOpenConfirmationWindow);
        }

        currentlyOpenConfirmationWindow = GameObject.Instantiate(Constants.Prefabs.PurchaseItemMenu, Constants.Persistant.InteractableUI);
        currentlyOpenConfirmationWindow.GetComponent<PurchaseItemMenu>().Initialize(this, item);

    }

    public void PurchaseItem(Item item)
    {
        if (Constants.Persistant.PlayerScript.Gold >= item.Price)
        {
            Constants.Persistant.PlayerScript.Gold -= item.Price;
            PurchasedItems.Add(item.Id);
            TransferItemTo(Constants.Persistant.PlayerScript.Inventory, item.Id, 1);
        }
    }

    public void SellItem(Item item)
    {
        Constants.Persistant.PlayerScript.Gold += item.Price;
        Constants.Persistant.PlayerScript.Inventory.TransferItemTo(this, item.Id, hasTransferBeenConfirmed: true, quantity: 1);
    }

    public override void OpenReceiveConfirmationMenu(Item item)
    {
        if (currentlyOpenConfirmationWindow != null)
        {
            GameObject.Destroy(currentlyOpenConfirmationWindow);
        }
        currentlyOpenConfirmationWindow = GameObject.Instantiate(Constants.Prefabs.PurchaseItemMenu, Constants.Persistant.InteractableUI);
        currentlyOpenConfirmationWindow.GetComponent<PurchaseItemMenu>().Initialize(this, item, buyMode: false);
    }
}
