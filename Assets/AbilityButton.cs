using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public int AbilityIndex;
    private Image _background;
    private Image background
    {
        get
        {
            if (_background == null)
            {
                _background = this.GetComponent<Image>();
            }
            return _background;
        }
    }
    private Image _outline;
    private Image outline
    {
        get
        {
            if (_outline == null)
            {
                _outline = transform.Find("Outline").GetComponent<Image>();
            }
            return _outline;
        }
    }
    private Image _icon;
    private Image icon
    {
        get
        {
            if (_icon == null)
            {
                _icon = transform.Find("Icon").GetComponent<Image>();
            }
            return _icon;
        }
    }
    private Text _text;
    private Text text
    {
        get
        {
            if (_text == null)
            {
                _text = transform.Find("Text").GetComponent<Text>();
            }
            return _text;
        }
    }
    private Skill skill;

    // Start is called before the first frame update
    void Start()
    {
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

        skill = AbilityIndex == 0 ?
            Constants.Persistant.PlayerScript.PrimarySkill :
            Constants.Persistant.PlayerScript.SecondarySkill;
    }
}
