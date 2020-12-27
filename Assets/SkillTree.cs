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
    }
}
