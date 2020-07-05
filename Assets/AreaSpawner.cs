using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    public Dictionary<string, Cow> SpawnedCows;
    public Area AreaType;
    public int AreaIndex;
    private const int MaxCows = 10;
    private List<GameObject> SpawnableCows;
    private Vector2 SpawnableAreaSize;
    private Vector3 AreaCenter;

    void Start()
    {
        AreaIndex = int.Parse(gameObject.name.Split('_')[1]);
        SpawnableAreaSize = new Vector2(45, 45);
        SpawnedCows = new Dictionary<string, Cow>();
        SpawnableCows = LoadSpawnableCows();
        AreaCenter = transform.position;

        SpawnCowsToMax();
        SpawnZoneGuardianIfNeeded();
    }

    void SpawnAreaBossIfNeeded()
    {

    }

    void Update()
    {
        SpawnCowsIfNeeded();
    }

    private const float timeBetweenSpawnChecks = 15f;
    private float lastSpawnCheckTime;
    private void SpawnCowsIfNeeded()
    {
        if (Time.time < lastSpawnCheckTime + timeBetweenSpawnChecks)
        {
            return;
        }

        CleanCowList();

        SpawnCowsToMax();

        lastSpawnCheckTime = Time.time;
    }

    private void SpawnCowsToMax()
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
            0f,
            Random.Range(-SpawnableAreaSize.y / 2, SpawnableAreaSize.y / 2));

        GameObject cow = Instantiate(
            SpawnableCows[Random.Range(0, SpawnableCows.Count)],
            position + AreaCenter,
            new Quaternion(),
            this.transform);
        cow.GetComponent<Cow>().Initialize();
        SpawnedCows.Add(cow.name, cow.GetComponent<Cow>());
    }

    private void CleanCowList()
    {
        List<string> cowsToRemove = new List<string>();
        foreach (string cowName in SpawnedCows.Keys){
            if (SpawnedCows[cowName] == null){
                cowsToRemove.Add(cowName);
            }
        }

        foreach(string cowName in cowsToRemove)
        {
            SpawnedCows.Remove(cowName);
        }
    }

    private List<GameObject> LoadSpawnableCows()
    {
        List<GameObject> cows = new List<GameObject>();
        foreach (CowType cow in WhatCowsSpawnInEachArea.Spawns[AreaType][AreaIndex])
        {
            cows.Add(Constants.Prefabs.CowPrefabs[cow]);
        }
        return cows;
    }

    private void SpawnZoneGuardianIfNeeded()
    {
        if (AreaIndex >= GameState.Data.HighestZoneUnlocked)
        {
            CowType type = WhatCowsSpawnInEachArea.ZoneGuardians[AreaType][AreaIndex];
            GameObject newCow = Instantiate(
                Constants.Prefabs.CowPrefabs[type], 
            AreaCenter,
            new Quaternion(),
            this.transform);
            newCow.GetComponent<Cow>().Initialize();
            newCow.GetComponent<Cow>().PromoteToZoneGuardian();
        }
    }
}
