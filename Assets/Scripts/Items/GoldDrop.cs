using System;
using System.Collections.Generic;
using UnityEngine;

public class GoldDrop : Drop
{
    public int Value;
    public override bool HasAutoPickup => true;
    public override int Quantity => Value;

    private readonly int[] CoinSizes = new int[] { 1, 4, 16 };
    private List<GameObject> coinPrefabs;

    public string Id;
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

    public GoldDrop(int low, int high)
    {
        Value = UnityEngine.Random.Range(low, high);
        coinPrefabs = new List<GameObject>();
        Id = Guid.NewGuid().ToString("N");
    }

    public override GameObject GetDropIndicator()
    {
        return null;
    }

    public override bool GiveDropToPlayer(Player player)
    {
        foreach (GameObject coin in coinPrefabs)
        {
            coin.GetComponent<PoolObject>().ReturnToPool();
        }

        player.Gold += Value;
        Value = 0;

        return true;
    }

    public override void SetModel(Transform container)
    {
        int remainingValue = Value;
        int coinSizeIndex = CoinSizes.Length - 1;
        while (remainingValue > 0 && coinPrefabs.Count < 3)
        {
            if (remainingValue >= CoinSizes[coinSizeIndex])
            {
                remainingValue -= CoinSizes[coinSizeIndex];
                GameObject coin = Pools.GoldPool.GetObject(coinSizeIndex);
                coin.transform.parent = container;
                Vector3 position = container.transform.position;
                position += new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.1f, .1f), UnityEngine.Random.Range(-.5f, .5f));
                coin.transform.position = position;
                coinPrefabs.Add(coin);
            }
            else
            {
                coinSizeIndex -= 1;
            }
        }
    }
}