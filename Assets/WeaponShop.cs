using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : Shop
{
    public override string StoreName => "Blacksmith";
    public override List<Item> StartingItems => new List<Item> { new Stick(), new Stick() };
}
