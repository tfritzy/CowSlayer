using UnityEngine;

public class GoldDrop : Drop 
{
    public int Value;
    public override bool HasAutoPickup => true;
    public override int Quantity => Value;

    private Sprite _icon;
    public override Sprite Icon
    {
        get
        {
            return GetDetails().icon;
        }
    }

    public GoldDrop(int low, int high)
    {
        Value = Random.Range(low, high);
    }

    private class GoldDetails
    {
        public int sizeIndex;
        public Sprite icon;
        public GameObject model;
    }

    public override GameObject GetDropIndicator()
    {
        return null;
    }

    public override bool GiveDropToPlayer(Player player)
    {
        player.Gold += Value;
        return true;
    }

    private GoldDetails _details;
    private GoldDetails GetDetails()
    {
        if (_details != null)
        {
            return _details;
        }

        GoldDetails details = new GoldDetails();
        if (Value >= 100000)
        {
            details.sizeIndex = 11;
        }
        else if (Value >= 10000)
        {
            details.sizeIndex = 10;
        }
        else if (Value >= 1000)
        {
            details.sizeIndex = 9;
        }
        else if (Value >= 1000)
        {
            details.sizeIndex = 8;
        }
        else if (Value >= 500)
        {
            details.sizeIndex = 7;
        }
        else if (Value >= 250)
        {
            details.sizeIndex = 6;
        }
        else if (Value >= 100)
        {
            details.sizeIndex = 5;
        }
        else if (Value >= 50)
        {
            details.sizeIndex = 4;
        }
        else if (Value >= 25)
        {
            details.sizeIndex = 3;
        }
        else if (Value >= 10)
        {
            details.sizeIndex = 2;
        }
        else if (Value >= 5)
        {
            details.sizeIndex = 1;
        }
        else
        {
            details.sizeIndex = 0;
        }

        details.model = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Gold}/{details.sizeIndex}");
        details.icon = Resources.Load<Sprite>($"{Constants.FilePaths.GoldIcons}/{details.sizeIndex}");
        return details;
    }

    public override void SetModel(Transform container)
    {
        GameObject.Instantiate(GetDetails().model, container.position, new Quaternion(), container);
    }
}