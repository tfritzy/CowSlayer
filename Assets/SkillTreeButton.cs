﻿using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{
    public SkillType skillType;

    private SkillTree parent;
    private Image background;
    private Image outline;
    private Image icon;
    private Text level;

    public void Setup(SkillType skillType, SkillTree parent)
    {
        this.skillType = skillType;
        this.parent = parent;
        background = transform.Find("Background").GetComponent<Image>();
        outline = transform.Find("Outline").GetComponent<Image>();
        icon = transform.Find("Icon").GetComponent<Image>();
        level = transform.Find("Level").GetComponent<Text>();
        level.text = "";
        icon.sprite = Constants.Skills[skillType].Icon;

        if (IsUnlocked())
        {
            outline.color = Constants.UI.Colors.LightBase;
            background.color = Constants.UI.Colors.Base;
            level.text = GameState.Data.SkillLevels[skillType].ToString();
        }
        else if (IsUnlockable())
        {
            background.color = ColorExtensions.Lighten(Color.blue);
            outline.color = Color.blue;
        } else 
        {
            outline.color = Color.grey;
            background.color = ColorExtensions.Lighten(Color.black);
        }
    }

    private bool IsUnlocked()
    {
        if (GameState.Data.SkillLevels.TryGetValue(skillType, out int skillLevel))
        {
            if (skillLevel > 0)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsUnlockable()
    {
        Skill skill = Constants.Skills[skillType];
        bool isUnlockable = true;
        foreach (SkillType type in skill.UnlockDependsOn.Keys)
        {
            GameState.Data.SkillLevels.TryGetValue(type, out int skillLevel);

            if (skillLevel < skill.UnlockDependsOn[type])
            {
                isUnlockable = false;
            }
        }

        return isUnlockable;
    }


    public void Click()
    {
        if (GameState.Data.SkillLevels.ContainsKey(skillType) == false)
        {
            GameState.Data.SkillLevels[skillType] = 0;
        }

        GameState.Data.SkillLevels[skillType] += 1;

        parent.RefreshButtonValues();
    }
}