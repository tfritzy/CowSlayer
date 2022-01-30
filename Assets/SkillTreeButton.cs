using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{
    public SkillType SkillType;
    public int ButtonIndex;
    public Skill Skill { get; private set; }

    private SkillTree parent;
    private Image background;
    private Image outline;
    private Image icon;
    private Text level;


    void Update()
    {
        if (IsSelectingAbility && icon != null)
        {
            Color lerpedColor = Color.Lerp(Color.white, Color.white / 2, Mathf.PingPong(Time.time, 1));
            icon.color = lerpedColor;
            outline.color = lerpedColor;
        }
    }

    public void Setup(SkillType skillType, SkillTree parent, int buttonIndex)
    {
        this.SkillType = skillType;
        this.Skill = Skill.BuildSkill(skillType, null);
        this.parent = parent;
        this.ButtonIndex = buttonIndex;
        background = transform.Find("Background").GetComponent<Image>();
        outline = transform.Find("Outline").GetComponent<Image>();
        icon = transform.Find("Icon").GetComponent<Image>();
        level = transform.Find("Level").GetComponent<Text>();
        level.text = "";
        icon.sprite = this.Skill.Icon;

        this.IsSelectingAbility = false;
        SetupColors();
    }


    private bool IsSelectingAbility;
    private int selectingIndex;
    public void SetSelectingAbility(bool starting, int abilityIndex)
    {
        IsSelectingAbility = starting;
        selectingIndex = abilityIndex;

        if (IsSelectingAbility == false)
        {
            SetupColors();
        }
    }

    public bool IsUnlocked()
    {
        if (GameState.Data.SkillLevels.TryGetValue(SkillType, out int skillLevel))
        {
            if (skillLevel > 0)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsUnlockable()
    {
        foreach (SkillType type in this.Skill.UnlockDependsOn)
        {
            GameState.Data.SkillLevels.TryGetValue(type, out int skillLevel);

            if (skillLevel == 0)
            {
                return false;
            }
        }

        return true;
    }

    public void Click()
    {
        if (IsSelectingAbility)
        {
            SelectAbility();
        }
        else
        {
            UpgradeSkill();
        }
    }

    private void SelectAbility()
    {
        if (IsUnlocked() == false)
        {
            return;
        }

        Constants.Persistant.PlayerScript.SetAbility(selectingIndex, SkillType);
        parent.StopSelectingAbility();
    }

    private void UpgradeSkill()
    {
        if (IsUnlockable() == false)
        {
            return;
        }

        if (GameState.Data.UnspentSkillPoints <= 0)
        {
            return;
        }

        if (GameState.Data.SkillLevels.ContainsKey(SkillType) == false)
        {
            GameState.Data.SkillLevels[SkillType] = 0;
        }

        GameState.Data.SkillLevels[SkillType] += 1;
        GameState.Data.UnspentSkillPoints -= 1;

        parent.RefreshButtonValues();
    }

    private void SetupColors()
    {
        if (IsUnlocked())
        {
            icon.color = Constants.UI.Colors.HighLight;
            outline.color = Constants.UI.Colors.HighLight;
            background.color = Constants.UI.Colors.BrightBase;
            level.text = GameState.Data.SkillLevels[SkillType].ToString();
        }
        else if (IsUnlockable())
        {
            icon.color = Constants.UI.Colors.VeryBrightBase;
            outline.color = Constants.UI.Colors.VeryBrightBase;
            background.color = Constants.UI.Colors.BrightBase;
        }
        else
        {
            icon.color = Color.grey;
            outline.color = Color.grey;
            background.color = new Color(0.25f, 0.25f, 0.25f, 1);
        }
    }
}
