using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSorcress : Player
{
    public enum SkillType {
        Firebolt,
        Fireball,
        Meteor
    }

    private Dictionary<SkillType, Skill> _skills;
    public Dictionary<SkillType, Skill> Skills
    {
        get {
            if (_skills == null)
            {
                _skills = new Dictionary<SkillType, Skill>()
                {
                    {
                        SkillType.Firebolt,
                        new FireBolt(),
                    }
                }
            }

            return _skills;
        }
    }
}
