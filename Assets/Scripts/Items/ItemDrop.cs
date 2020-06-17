using UnityEngine;

public class ItemDrop : Drop 
{
    public Item Item;
 
    public ItemDrop(Item item)
    {
        this.Item = item;
    }
    
    public override bool GiveDropToPlayer(Player player)
    {
        if (!player.Inventory.IsFull())
        {
            player.Inventory.AddItem(this.Item);
            return true;
        }

        return false;
    }

    public override void SetModel(Transform container)
    {
        GameObject.Instantiate(Item.Prefab, container.position, new Quaternion(), container);
    }
}