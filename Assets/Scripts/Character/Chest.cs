using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, Interactable
{
    public ItemGroup ItemGroup;
    
    void Start() {
        this.ItemGroup = new ChestItemGroup("Chest");
        this.ItemGroup.AddItems(new List<Item>() {new GoldRing(), new HornedHelm(), new IceRing(),
            new IronPlatelegs(), new LeatherBody(), new NorthstarAmulet()});
    }

    public void Interact()
    {
        Constants.GameObjects.PlayerScript.Inventory.OpenMenu(.33f, this.ItemGroup);
        this.ItemGroup.OpenMenu(.75f, Constants.GameObjects.PlayerScript.Inventory);
    }
}