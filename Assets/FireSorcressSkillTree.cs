using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSorcressSkillTree : SkillTree
{
    protected override List<SkillType> Skills 
    {
        get {
            return FireSorcress.Abilities;
        }
    }
}
