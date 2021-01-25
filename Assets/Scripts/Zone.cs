using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Zone
{
    public AreaSpawner Spawner;
    public GameObject Gate;
    public Transform Transform;
    public int ZoneIndex;

    public Zone(Transform zoneGameObject, int index)
    {
        Spawner = zoneGameObject.GetComponent<AreaSpawner>();
        Gate = zoneGameObject.transform.Find("Wall").Find("Gate").gameObject;
        Transform = zoneGameObject.transform;
        this.ZoneIndex = index;
        Reset();
    }

    public void SetZone(int index)
    {
        this.ZoneIndex = index;
        Spawner.SwitchZones(index);
        Reset();
    }

    public void Reset()
    {
        this.Transform.gameObject.name = "Zone_" + ZoneIndex;
        if (GameState.Data.HighestZoneUnlocked >= ZoneIndex)
        {
            this.Gate.SetActive(false);
        }
        else
        {
            this.Gate.SetActive(true);
        }
    }
}

