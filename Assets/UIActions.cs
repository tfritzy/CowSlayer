using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActions : MonoBehaviour
{
    public void CloseAllWindows()
    {
        Debug.Log("Close All");
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(Constants.Tags.InteractableUI))
        {
            Destroy(go);
        }
    }
}
