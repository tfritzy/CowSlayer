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

    // Start is called before the first frame update
    void Start()
    {
        background = this.GetComponent<Image>();
        outline = transform.Find("Outline").GetComponent<Image>();
        icon = transform.Find("Icon").GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        FormatButton();
    }

    // Update is called once per frame
    void Update()
    {

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
