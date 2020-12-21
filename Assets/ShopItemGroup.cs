using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemGroup : ItemGroup
{
    private HashSet<string> PurchasedItems;
    public override int MaxSize => 20;
    public override string UIPrefabName => "ChestUI";
    public override bool RequiresReceiveConfirmation => true;

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
        Item item = GetItem(itemId);
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
