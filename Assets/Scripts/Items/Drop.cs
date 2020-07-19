using System;
using UnityEngine;

public abstract class Drop 
{
    protected string dropId;
    public abstract bool HasAutoPickup { get; }

    public Drop()
    {
        dropId = Guid.NewGuid().ToString("N");
    }

    public abstract GameObject GetDropIndicator();
    public abstract bool GiveDropToPlayer(Player player);
    public abstract void SetModel(Transform container);
    public abstract int Quantity { get; }
    public abstract Sprite Icon { get; }

    public override int GetHashCode()
    {
        return dropId.GetHashCode();
    }
}