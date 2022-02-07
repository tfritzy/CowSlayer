using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, Interactable
{
    public ItemGroup ItemGroup;

    void Start()
    {
        this.ItemGroup = new ChestItemGroup("Chest");
        this.ItemGroup.AddItems(
            new List<Item>() { new GoldRing(3), new HornedHelm(1), new IronPlatelegs(5), new LeatherBody(3) }
        );
    }

    public void Interact()
    {
        Constants.Persistant.PlayerScript.Inventory.OpenMenu(.33f, this.ItemGroup);
        this.ItemGroup.OpenMenu(.75f, Constants.Persistant.PlayerScript.Inventory);
    }
}