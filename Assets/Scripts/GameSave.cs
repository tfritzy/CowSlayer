using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameSave
{
    public int HighestZoneUnlocked;
    public int PlayerLevel;
    public int PlayerXP;
    public string CharacterFaction;
    public int UnspentSkillPoints;
    private Dictionary<SkillType, int> _skillTypes;
    public Dictionary<SkillType, int> SkillLevels{
        get {
            if (_skillTypes == null)
            {
                _skillTypes = new Dictionary<SkillType, int>();
            }

            return _skillTypes;
        }
    }
}
