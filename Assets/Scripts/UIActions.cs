using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActions
{
    public static void CloseAllWindows()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(Constants.Tags.InteractableUI))
        {
            GameObject.Destroy(go);
        }
    }
}
