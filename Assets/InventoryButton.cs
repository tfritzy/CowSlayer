using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public static bool IsOpen = false;
    public void OpenInventory()
    {
        UIActions.CloseAllWindows();
        if (!IsOpen)
        {
            Constants.Persistant.PlayerScript.OpenInventory();
        }
        IsOpen = !IsOpen;
    }

    public void CloseAllWindows()
    {
        UIActions.CloseAllWindows();
        IsOpen = false;
    }
}
