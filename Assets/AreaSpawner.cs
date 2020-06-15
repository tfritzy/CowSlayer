using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    public List<GameObject> SpawnedCows;
    public string AreaName;
    private const int MaxCows = 20;
    private GameObject[] SpawnableCows;
    private Vector2 SpawnableAreaSize;
    private Vector3 AreaCenter;

    void Start()
    {
        AreaName = gameObject.name;
        SpawnableAreaSize = new Vector2(45, 45);
        SpawnedCows = new List<GameObject>();
        SpawnableCows = LoadSpawnableCows();
        AreaCenter = transform.Find("Ground").position;
    }

    void Update()
    {
        SpawnCowsIfNeeded();
    }

    private void SpawnCowsIfNeeded()
    {
        for (int i = SpawnedCows.Count; i < MaxCows; i++)
        {
            SpawnACow();
        }
    }

    private void SpawnACow()
    {
        Vector3 position = new Vector3(
            Random.Range(-SpawnableAreaSize.x / 2, SpawnableAreaSize.x / 2),
            Constants.MapParameters.BlockYPos,
            Random.Range(-SpawnableAreaSize.y / 2, SpawnableAreaSize.y / 2));

        GameObject cow = Instantiate(
            SpawnableCows[Random.Range(0, SpawnableCows.Length)],
            position + AreaCenter,
            new Quaternion(),
            Constants.GameObjects.CowParent);
        SpawnedCows.Add(cow);
    }

    private GameObject[] LoadSpawnableCows()
    {
        return Resources.LoadAll<GameObject>($"{Constants.FilePaths.AreaSpawns}/{AreaName}");
    }
}
