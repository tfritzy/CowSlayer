using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSorcress : Player
{
    public static List<SkillType> Abilities = new List<SkillType>()
    {
        SkillType.Firebolt,
        SkillType.Fireball,
        SkillType.Meteor
    };

}
