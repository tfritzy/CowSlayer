using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour, Interactable
{
    public ShopItemGroup ShopItems;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        ShopItems = new ShopItemGroup("PotionStore");
        ShopItems.AddItem(new Stick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (!ShopItems.IsMenuOpen())
        {
            Constants.GameObjects.PlayerScript.Inventory.OpenMenu(.33f, this.ShopItems);
            this.ShopItems.OpenMenu(.75f, Constants.GameObjects.PlayerScript.Inventory);
            isOpen = true;
        }
    }
}
