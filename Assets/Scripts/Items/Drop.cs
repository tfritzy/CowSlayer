using System;
using UnityEngine;

public abstract class Drop 
{
    protected string dropId;

    public Drop()
    {
        dropId = Guid.NewGuid().ToString("N");
    }

    public abstract bool GiveDropToPlayer(Player player);
    public abstract void SetModel(Transform container);

    public override int GetHashCode()
    {
        return dropId.GetHashCode();
    }
}