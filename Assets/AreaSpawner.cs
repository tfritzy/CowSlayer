using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    public Dictionary<string, Cow> SpawnedCows;
    public Area AreaType;
    public int AreaIndex;
    private const int MaxCows = 5;
    private List<GameObject> SpawnableCows;
    private Vector2 SpawnableAreaSize;
    private Vector3 AreaCenter;

    void Start()
    {
        AreaIndex = int.Parse(gameObject.name.Split('_')[1]);
        SpawnableAreaSize = new Vector2(45, 45);
        SpawnedCows = new Dictionary<string, Cow>();
        SpawnableCows = LoadSpawnableCows();

        SwitchZones(AreaIndex);
    }

    void Update()
    {
        SpawnCowsIfNeeded();
    }

    public void SwitchZones(int newZoneIndex)
    {
        AreaCenter = transform.position;
        AreaCenter.y = Constants.WorldProperties.GroundLevel;
        this.AreaIndex = newZoneIndex;
        foreach (Cow cow in SpawnedCows.Values)
        {
            Destroy(cow.gameObject);
        }

        SpawnedCows = new Dictionary<string, Cow>();

        SpawnCowsToMax();
        SpawnZoneGuardianIfNeeded();
    }

    private const float timeBetweenSpawnChecks = 5f;
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
        GameObject cow = Instantiate(
            SpawnableCows[Random.Range(0, SpawnableCows.Count)],
            Vector3.zero,
            new Quaternion(),
            this.transform);

        Vector3 position = new Vector3(
            Random.Range(-SpawnableAreaSize.x / 2, SpawnableAreaSize.x / 2),
            Constants.WorldProperties.GroundLevel + cow.GetComponent<Cow>().Body.VerticalBounds.x + .01f,
            Random.Range(-SpawnableAreaSize.y / 2, SpawnableAreaSize.y / 2))
            + AreaCenter;

        cow.GetComponent<Cow>().transform.position = position;
        position.y = 0;
        cow.transform.position = position;

        cow.GetComponent<Cow>().Initialize();
        SpawnedCows.Add(cow.name, cow.GetComponent<Cow>());
    }

    private void CleanCowList()
    {
        List<string> cowsToRemove = new List<string>();
        foreach (string cowName in SpawnedCows.Keys)
        {
            if (SpawnedCows[cowName] == null)
            {
                cowsToRemove.Add(cowName);
            }
        }

        foreach (string cowName in cowsToRemove)
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
            SpawnedCows[newCow.name] = newCow.GetComponent<Cow>();
        }
    }
}
