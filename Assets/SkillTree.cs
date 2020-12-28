using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillTree : MonoBehaviour
{
    protected virtual List<SkillType> Skills { get; }
    public Dictionary<SkillType, Transform> Buttons;

    void Start()
    {
        transform.Find("Background").GetComponent<Image>().color = Constants.UI.Colors.Base;
        transform.Find("Outline").GetComponent<Image>().color = Constants.UI.Colors.BrightBase;

        GameObject.Instantiate(Constants.Prefabs.CloseMenuButton, Constants.Persistant.InteractableUI);
        RefreshButtonValues();
    }

    public void RefreshButtonValues()
    {
        Buttons = new Dictionary<SkillType, Transform>();
        for (int i = 0; i < Skills.Count; i++)
        {
            Buttons[Skills[i]] = transform.Find($"SkillTreeSlot{i}");
            Buttons[Skills[i]].GetComponent<SkillTreeButton>().Setup(Skills[i], this, i);
        }

        transform.Find("SkillPoints").GetComponent<Text>().text = $"Skill Points: {GameState.Data.UnspentSkillPoints}";
    }
}
