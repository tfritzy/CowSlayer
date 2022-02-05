using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShop : Shop
{

    private string storeName = "Potion Shop";
    public override string StoreName => storeName;
    public override List<Item> StartingItems => new List<Item> { new SmallHealthPotion() { Quantity = 100 }, new SmallManaPotion() { Quantity = 100 } };
}
