using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, Interactable
{
    public const int numRows = 4;
    public const int numCols = 5;
    public ItemGroup ItemGroup;
    
    void Start() {
        this.ItemGroup = new ChestItemGroup();
        this.ItemGroup.AddItems(new List<Item>() {new GoldRing(), new HornedHelm(), new IceRing(), new IronBracer(),
            new IronPlatelegs(), new LeatherBody(), new NorthstarAmulet()});
    }

    public void Interact()
    {
        Constants.GameObjects.PlayerScript.Inventory.OpenMenu(.75f, this.ItemGroup);
        this.ItemGroup.OpenMenu(.33f, Constants.GameObjects.PlayerScript.Inventory);
    }
}