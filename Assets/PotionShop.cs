using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionShop : Shop
{
    public override string StoreName => "Potion Shop";
    public override List<Item> StartingItems => new List<Item> { new SmallHealthPotion() };
}
