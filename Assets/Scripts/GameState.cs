using UnityEngine;

public class GameState : MonoBehaviour
{
    private void Awake()
    {
        Load();
    }

    public static void Load()
    {
        _data = new GameSave();
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

