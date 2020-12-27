using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillTree : MonoBehaviour
{
    protected virtual List<SkillType> Skills {get;}
    private List<Transform> buttons;

    void Start()
    {
        transform.Find("Background").GetComponent<Image>().color = Constants.UI.Colors.Base;
        transform.Find("Outline").GetComponent<Image>().color = Constants.UI.Colors.BrightBase;

        GameObject.Instantiate(Constants.Prefabs.CloseMenuButton, Constants.Persistant.InteractableUI);
        RefreshButtonValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshButtonValues()
    {
        buttons = new List<Transform>();
        for (int i = 0; i < Skills.Count; i++)
        {
            buttons.Add(transform.Find($"SkillTreeSlot{i}"));
            buttons[i].GetComponent<SkillTreeButton>().Setup(Skills[i], this);
        }
        
        transform.Find("SkillPoints").GetComponent<Text>().text = $"Skill Points: {GameState.Data.UnspentSkillPoints}";
    }
}
