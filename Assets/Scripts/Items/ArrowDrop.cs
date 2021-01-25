using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDrop : StackableDrop
{
    private static Sprite _icon;
    public override Sprite Icon
    {
        get
        {
            if (_icon == null)
            {
                _icon = Resources.Load<Sprite>($"{Constants.FilePaths.Icons}/Arrow");
            }

            return _icon;
        }
    }

    protected override Dictionary<int, DropType> SizeMap
    {
        get { return ArrowSizeMap; }
    }
    public override int MaxPrefabs => 1;
    private readonly Dictionary<int, DropType> ArrowSizeMap = new Dictionary<int, DropType>()
    {
        { 1, DropType.Single_Arrow },
        { 2, DropType.Quiver },
    };

    public ArrowDrop(int low, int high) : base(low, high)
    {
    }

    public override GameObject GetDropIndicator()
    {
        return null;
    }

    public override bool GiveDropToPlayer(Player player)
    {
        Arrow arrow = new Arrow();
        arrow.Quantity = Value;
        player.Inventory.AddItem(arrow);

        base.GiveDropToPlayer(player);

        return true;
    }
}