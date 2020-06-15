using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public bool IsOpen = false;
    public void OpenInventory()
    {
        UIActions.CloseAllWindows();
        if (!IsOpen)
        {
            Constants.GameObjects.PlayerScript.OpenInventory();
        }
        IsOpen = !IsOpen;
    }
}
