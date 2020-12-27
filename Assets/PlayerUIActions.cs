using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIActions : MonoBehaviour
{
    public void OpenInventory()
    {
        UIActions.CloseAllWindows();
        Constants.Persistant.PlayerScript.OpenInventory();
    }

    public void CloseAllWindows()
    {
        UIActions.CloseAllWindows();
    }

    private GameObject _skillMenuPrefab;
    public void OpenSkillsMenu()
    {
        UIActions.CloseAllWindows();
        
        if (_skillMenuPrefab == null)
        {
            _skillMenuPrefab = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/{GameState.Data.CharacterFaction}SkillTree");
        }

        GameObject.Instantiate(_skillMenuPrefab, Constants.Persistant.InteractableUI);
    }
}
