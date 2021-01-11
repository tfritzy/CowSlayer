using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public const int NumZonesPerMap = 5;
    private List<Zone> Zones;

    private void Start()
    {
        Transform chunkParent = Constants.Persistant.ZoneManager.transform;
        Zones = new List<Zone>();
        for (int i = 0; i < NumZonesPerMap; i++)
        {
            Transform zone = chunkParent.Find($"Zone_{i}");

            if (zone == null)
            {
                break;
            }

            Zones.Add(new Zone(zone));
        }

        UnlockZones();
    }

    public void UnlockZones()
    {
        for (int i = 0; i <= GameState.Data.HighestZoneUnlocked; i++)
        {
            UnlockGate(Zones[i].Gate);
        }
    }

    private void UnlockGate(GameObject gate)
    {
        Destroy(gate);
    }
}

