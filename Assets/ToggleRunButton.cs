using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

public class ToggleRunButton : MonoBehaviour
{
    private Color disabledColor = ColorExtensions.Create("292929");
    private Color enabledColor = ColorExtensions.Create("EED541");
    private Image icon;

    private void Start()
    {
        this.icon = this.transform.Find("Icon").GetComponent<Image>();
        icon.color = Constants.Persistant.PlayerScript.IsRunning ? enabledColor : disabledColor;
    }

    public void ToggleRun()
    {
        Constants.Persistant.PlayerScript.ToggleRunStatus();
        icon.color = Constants.Persistant.PlayerScript.IsRunning ? enabledColor : disabledColor;
    }
}
