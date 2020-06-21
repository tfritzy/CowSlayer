using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveButton : MonoBehaviour
{
    public void RevivePlayer()
    {
        Constants.GameObjects.PlayerScript.Respawn();
    }
}
