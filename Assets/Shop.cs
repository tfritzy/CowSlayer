using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : MonoBehaviour, Interactable
{
    public ShopItemGroup ShopItems;
    public abstract string StoreName { get; }
    public abstract List<Item> StartingItems { get; }

    void Start()
    {
        ShopItems = new ShopItemGroup(StoreName);
        ShopItems.AddItems(StartingItems);
    }

    void Update()
    {
        
    }

    public void Interact()
    {
        if (!ShopItems.IsMenuOpen())
        {
            Constants.Persistant.PlayerScript.Inventory.OpenMenu(.33f, this.ShopItems);
            this.ShopItems.OpenMenu(.75f, Constants.Persistant.PlayerScript.Inventory);
        }
    }
}
