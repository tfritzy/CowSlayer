using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, Interactable
{
    public ShopItemGroup ShopItems;

    void Start()
    {
        ShopItems = new ShopItemGroup("PotionStore");
        ShopItems.AddItem(new Stick());
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
