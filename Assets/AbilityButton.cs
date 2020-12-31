using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public int AbilityIndex;
    private Image background;
    private Image outline;
    private Image icon;
    private Text text;
    private Skill skill;

    // Start is called before the first frame update
    void Start()
    {
        background = this.GetComponent<Image>();
        outline = transform.Find("Outline").GetComponent<Image>();
        icon = transform.Find("Icon").GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        skill = AbilityIndex == 0 ? 
                    Constants.Persistant.PlayerScript.PrimarySkill : 
                    Constants.Persistant.PlayerScript.SecondarySkill;
        FormatButton();
    }

    // Update is called once per frame
    void Update()
    {
        float cooldown = skill.RemainingCooldown();
        if (cooldown > 0)
        {
            text.text = cooldown.ToString("N1");
            icon.color = Color.grey;
        }
        else 
        {
            icon.color = Constants.UI.Colors.HighLight;
            text.text = "";
        }
    }

    public void FormatButton()
    {
        icon.color = Constants.UI.Colors.HighLight;
        outline.color = Constants.UI.Colors.BrightBase;
        background.color = Constants.UI.Colors.BrightBase;

        if (AbilityIndex == 0)
        {
            icon.sprite = Constants.Persistant.PlayerScript.PrimarySkill.Icon;
        }
        else
        {
            icon.sprite = Constants.Persistant.PlayerScript.SecondarySkill.Icon;
        }
    }
}
