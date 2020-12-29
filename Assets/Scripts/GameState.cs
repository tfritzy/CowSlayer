using UnityEngine;

public class GameState : MonoBehaviour
{
    private void Awake()
    {
        Load();
    }

    public static void Load()
    {
        // Initialize Test Data
        _data = new GameSave()
        {
            CharacterFaction = "FireSorcress",
            HighestZoneUnlocked = 0,
            PlayerLevel = 1,
            PlayerXP = 3,
            UnspentSkillPoints = 3,
            PrimarySkill = SkillType.Firebolt,
            SecondarySkill = SkillType.Meteor,
        };

        _data.PlayerLevel = Mathf.Max(_data.PlayerLevel, 1);
    }

    private static GameSave _data;
    public static GameSave Data
    {
        get
        {
            if (_data == null)
            {
                Load();
            }
            return _data;
        }
    }
}

