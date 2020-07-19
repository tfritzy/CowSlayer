using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : Drop 
{
    public Item Item;
    public override bool HasAutoPickup => false;
    public override int Quantity => 1;

    public ItemDrop(Item item)
    {
        this.Item = item;
        
    }

    public override GameObject GetDropIndicator()
    {
        GameObject dropIndicator = GameObject.Instantiate(DropIndicator, Constants.Persistant.InteractableUI);
        dropIndicator.GetComponent<Button>().image.color = Item.GetRarityColor();
        dropIndicator.transform.Find("Icon").GetComponent<Image>().sprite = Item.GetIcon();
        return dropIndicator;
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

    private static GameObject _dropIndicator;
    protected static GameObject DropIndicator
    {
        get
        {
            if (_dropIndicator == null)
            {
                _dropIndicator = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/ItemPickupIndicator");
            }
            return _dropIndicator;
        }
    }
}