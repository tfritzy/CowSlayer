using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldDrop : StackableDrop
{
    protected override Dictionary<int, DropType> SizeMap
    {
        get { return CoinSizeMap; }
    }

    private readonly Dictionary<int, DropType> CoinSizeMap = new Dictionary<int, DropType>()
    {
        { 1, DropType.Gold_0 },
        { 4, DropType.Gold_1 },
        { 8, DropType.Gold_2 },
        { 16, DropType.Gold_3 },
    };

    private static Sprite _icon;
    public override Sprite Icon
    {
        get
        {
            if (_icon == null)
            {
                _icon = Resources.Load<Sprite>($"{Constants.FilePaths.Icons}/GoldCoin");
            }

            return _icon;
        }
    }
    public override int MaxPrefabs => 2;
    public GoldDrop(int low, int high) : base(low, high) { }

    public override GameObject GetDropIndicator()
    {
        return null;
    }

    public override bool GiveDropToPlayer(Player player)
    {
        player.Gold += Value;
        base.GiveDropToPlayer(player);
        return true;
    }
}