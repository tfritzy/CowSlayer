using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItemMenu : MonoBehaviour
{
    public Item Item;
    public string ItemId;
    public int Price;
    public ShopItemGroup OwningItemGroup;
    public bool BuyMode;
    private Text description;
  

    public void Initialize(ShopItemGroup owner, Item item, bool buyMode = true)
    {
        Item = item;
        ItemId = item.Id;
        Price = item.Price;
        OwningItemGroup = owner;
        description = transform.Find("Description").GetComponent<Text>();
        BuyMode = buyMode;
        if (BuyMode)
        {
            if (item.Price > Constants.GameObjects.PlayerScript.Gold)
            {
                description.text = $"Not enough gold. This costs {item.Price} but you have {Constants.GameObjects.PlayerScript.Gold}";
                transform.Find("YesButton").GetComponent<Button>().interactable = false;
            }
            else
            {
                description.text = $"Purchase {Item.Name} for {Item.Price} Gold?";
            }
        } else
        {
            description.text = $"Sell {Item.Name} for {Item.Price} Gold?";
        }
        
    }

    public void Accept()
    {
        if (BuyMode)
        {
            OwningItemGroup.PurchaseItem(Item);
        } else
        {
            OwningItemGroup.SellItem(Item);
        }
        Destroy(this.gameObject);
    }

    public void Decline()
    {
        Destroy(this.gameObject);
    }
}
