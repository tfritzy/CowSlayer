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
        FormatAbilitySelectButtons();
    }

    public void RefreshButtonValues()
    {
        Buttons = new Dictionary<SkillType, Transform>();
        for (int i = 0; i < Skills.Count; i++)
        {
            Buttons[Skills[i]] = transform.Find($"SkillTreeSlot{i}");
            SkillTreeButton button = Buttons[Skills[i]].GetComponent<SkillTreeButton>();
            button.Setup(Skills[i], this, i);

            foreach (SkillType sourceType in Constants.GetSkill(button.SkillType).UnlockDependsOn)
            {
                Transform sourceButton = Buttons[sourceType];
                int sourceIndex = sourceButton.GetComponent<SkillTreeButton>().ButtonIndex;
                Transform link = transform.Find($"Link {sourceIndex}-{i}");

                if (button.IsUnlocked())
                {
                    link.GetComponent<Image>().color = Constants.UI.Colors.HighLight;
                }
                else if (button.IsUnlockable())
                {
                    link.GetComponent<Image>().color = Constants.UI.Colors.VeryBrightBase;
                }
                else
                {
                    link.GetComponent<Image>().color = Color.grey;
                }
            }
        }

        transform.Find("SkillPoints").GetComponent<Text>().text = $"Skill Points: {GameState.Data.UnspentSkillPoints}";
    }

    public void SetSelectingAbility(int abilityIndex)
    {
        foreach (Transform button in Buttons.Values)
        {
            if (button.GetComponent<SkillTreeButton>().IsUnlocked())
            {
                button.GetComponent<SkillTreeButton>().SetSelectingAbility(true, abilityIndex);
            }
        }
    }

    public void StopSelectingAbility()
    {
        foreach (Transform button in Buttons.Values)
        {
            button.GetComponent<SkillTreeButton>().SetSelectingAbility(false, 0);
        }

        FormatAbilitySelectButtons();
    }

    private void FormatAbilitySelectButtons()
    {
        for (int i = 0; i < 2; i++)
        {
            Transform button = transform.Find($"AbilitySelect{i}");
            button.Find("Icon").GetComponent<Image>().color = Constants.UI.Colors.HighLight;
            button.Find("Icon").GetComponent<Image>().sprite = GetSkillIcon(i);
            button.Find("Outline").GetComponent<Image>().color = Constants.UI.Colors.BrightBase;
            button.Find("Background").GetComponent<Image>().color = Constants.UI.Colors.BrightBase;
        }
    }

    private Sprite GetSkillIcon(int abilityIndex)
    {
        if (abilityIndex == 0)
        {
            return Constants.Persistant.PlayerScript.PrimarySkill.Icon;
        }
        else
        {
            return Constants.Persistant.PlayerScript.SecondarySkill.Icon;
        }
    }
}
