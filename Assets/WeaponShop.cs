using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : Shop
{

    private string storeName = "Blacksmith";
    public override string StoreName => storeName;
    public override List<Item> StartingItems => new List<Item> { new Stick(4), new Stick(3) };
}
