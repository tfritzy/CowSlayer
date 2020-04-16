using UnityEngine;
using System.Collections;
using System;

public class ChestButton : MonoBehaviour
{
    public ItemGroup SourceItemGroup;
    public ItemGroup TargetItemGroup;
    public string ItemId;

    public void TransferItem()
    {
        if (TargetItemGroup == null)
        {
            return;
        }
        if (ItemId == null)
        {
            throw new NullReferenceException("Item id must have a value to call this method");
        }
        if (SourceItemGroup == null)
        {
            throw new NullReferenceException("SourceItemGroup must have a value to call this method");
        }

        SourceItemGroup.TransferItem(TargetItemGroup, ItemId);
    }
}
