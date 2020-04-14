using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, Interactable
{
    public const int numRows = 4;
    public const int numCols = 5;
    public ItemGroup ItemGroup;
    
    void Start() {
        this.ItemGroup = new ItemGroup(20, new Vector2(5, 4), "ChestUI");
        this.ItemGroup.Items = new List<Item>() {new TestItem(), new TestItem(), new TestItem(), new TestItem()};
    }

    public void Interact()
    {
        Constants.GameObjects.Player.GetComponent<Player>().OpenInventory();
        this.ItemGroup.OpenMenu(.5f);
    }
}