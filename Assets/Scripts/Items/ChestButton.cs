using UnityEngine;
using System.Collections;
using System;

public class ChestButton : MonoBehaviour
{
    public ItemGroup SourceItemGroup;
    public ItemGroup TargetItemGroup;
    protected Item Item;

    private bool isSelected;
    private bool areDetailsOpen;
    private GameObject itemDetailsInst;
    private float startPressTime;
    private const float holdTimeForDetails = .5f;
    public void Update()
    {
        if (!isSelected || Item == null)
        {
            return;
        }

        if (Time.time > holdTimeForDetails + startPressTime 
            && areDetailsOpen == false)
        {
            ShowDetails();
        }
    }

    public void SetItem(Item item)
    {
        this.Item = item;
    }

    private void Start()
    {
        isSelected = false;
        areDetailsOpen = false;
    }

    public void StartPress()
    {
        startPressTime = Time.time;
        isSelected = true;
    }

    public void EndPress()
    {
        if (areDetailsOpen)
        {
            CloseDetails();
        }
        else
        {
            TransferItem();
        }
    }

    public void TransferItem()
    {
        if (TargetItemGroup == null)
        {
            return;
        }
        if (Item == null)
        {
            return;
        }
        if (SourceItemGroup == null)
        {
            throw new NullReferenceException("SourceItemGroup must have a value to call this method");
        }
        
        SourceItemGroup.TransferItem(TargetItemGroup, Item.Id);
        isSelected = false;
    }

    public void ShowDetails()
    {
        Destroy(itemDetailsInst);
        itemDetailsInst = Item.ShowItemDetailsPage();
        Debug.Log("Showing Item Details");
        areDetailsOpen = true;
    }

    public void CloseDetails()
    {
        Destroy(itemDetailsInst);
        Debug.Log("Closing Details");
        areDetailsOpen = false;
        isSelected = false;
    }
}
