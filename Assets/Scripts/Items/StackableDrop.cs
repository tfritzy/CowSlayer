using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StackableDrop : Drop
{
    public int Value;
    public override bool HasAutoPickup => true;
    public override int Quantity => Value;
    public abstract int MaxPrefabs { get; }
    private List<int> _sizes;
    private List<int> Sizes
    {
        get
        {
            if (_sizes == null)
            {
                _sizes = SizeMap.Keys.ToList();
            }
            return _sizes;
        }
    }

    protected abstract Dictionary<int, DropType> SizeMap { get; }

    private List<GameObject> prefabs;

    public string Id;

    public StackableDrop(int low, int high)
    {
        Value = UnityEngine.Random.Range(low, high);
        prefabs = new List<GameObject>();
        Id = Guid.NewGuid().ToString("N");
    }

    public override bool GiveDropToPlayer(Player player)
    {
        foreach (GameObject prefab in prefabs)
        {
            prefab.GetComponent<PoolObject>().ReturnToPool();
        }
        Value = 0;

        return true;
    }

    public override GameObject GetDropIndicator()
    {
        return null;
    }

    public override void SetModel(Transform container)
    {
        int remainingValue = Value;
        int sizeIndex = SizeMap.Count - 1;
        while (remainingValue > 0 && prefabs.Count < MaxPrefabs)
        {
            if (remainingValue >= Sizes[sizeIndex])
            {
                remainingValue -= Sizes[sizeIndex];
                GameObject drop = Pools.DropPool.GetObject((int)SizeMap[Sizes[sizeIndex]]);
                drop.transform.parent = container;
                Vector3 position = container.transform.position;
                position += new Vector3(UnityEngine.Random.Range(-.5f, .5f), UnityEngine.Random.Range(-.1f, .1f), UnityEngine.Random.Range(-.5f, .5f));
                drop.transform.position = position;
                prefabs.Add(drop);
            }
            else
            {
                sizeIndex -= 1;
            }
        }
    }
}