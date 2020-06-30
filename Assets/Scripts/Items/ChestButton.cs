using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

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

    void Start()
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
        itemDetailsInst.transform.position = GetDetailPageTargetPos();
        areDetailsOpen = true;
    }

    public void CloseDetails()
    {
        Destroy(itemDetailsInst);
        areDetailsOpen = false;
        isSelected = false;
    }

    private Vector3 GetInputPosition()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        } else
        {
            return Input.mousePosition;
        }
    }

    private Vector2 GetDetailPageTargetPos()
    {
        float quarterScreen = Screen.width / 4;
        Vector2 inputPosition = GetInputPosition();

        // Put it on left side if finger is on right, or vice versa
        float leftOrRightHalf = inputPosition.x > Screen.width / 2 ? -1 : 1;

        // Limit how far up or down the page can be on the screen, to keep everything visible
        float yPos = Mathf.Min(inputPosition.y, Screen.height * .8f);
        yPos = Mathf.Max(yPos, Screen.height * .2f);

        return new Vector2(Screen.width / 2 + quarterScreen * leftOrRightHalf, yPos);
    }
}
