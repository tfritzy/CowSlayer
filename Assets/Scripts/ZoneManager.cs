using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public const int NumZonesSpawnedSimultaneously = 3;
    private LinkedList<Zone> Zones;
    private float DistBetweenZones;
    private float BasePosition;
    private int CurSpawnedMiddleIndex = 1;

    private void Start()
    {
        Transform chunkParent = Constants.Persistant.ZoneManager.transform;
        Zones = new LinkedList<Zone>();
        for (int i = 0; i < NumZonesSpawnedSimultaneously; i++)
        {
            Transform zone = chunkParent.Find($"Zone_{i}");

            if (zone == null)
            {
                break;
            }

            Zones.AddLast(new Zone(zone, i));
        }

        BasePosition = transform.Find("Base").transform.position.z;

        DistBetweenZones = (Zones.Last.Value.Transform.position.z - Zones.First.Value.Transform.position.z) / (Zones.Count - 1);
    }

    void Update()
    {
        KeepZonesAroundPlayer();
    }

    int curPlayerZone;
    private void KeepZonesAroundPlayer()
    {
        float distFromBase = Constants.Persistant.Player.transform.position.z - BasePosition;
        int curZone = (int)((distFromBase - DistBetweenZones / 2) / DistBetweenZones);
        curZone = Math.Max(curZone, 0);
        int direction = curZone > CurSpawnedMiddleIndex ? 1 : -1;

        while (ShouldSpawnZone(curZone))
        {
            Zone zoneBeingMoved;
            if (direction == 1)
            {
                zoneBeingMoved = Zones.First.Value;
                zoneBeingMoved.Transform.position = Zones.Last.Value.Transform.position + Vector3.forward * DistBetweenZones;
                Zones.RemoveFirst();
                Zones.AddLast(zoneBeingMoved);
            }
            else
            {
                zoneBeingMoved = Zones.Last.Value;
                zoneBeingMoved.Transform.position = Zones.First.Value.Transform.position + Vector3.back * DistBetweenZones;
                Zones.RemoveLast();
                Zones.AddFirst(zoneBeingMoved);
            }

            CurSpawnedMiddleIndex += direction;
            zoneBeingMoved.SetZone(CurSpawnedMiddleIndex + direction);
        }
    }

    private bool ShouldSpawnZone(int playerIndex)
    {
        if (playerIndex > CurSpawnedMiddleIndex)
        {
            return true;
        }

        if (playerIndex < CurSpawnedMiddleIndex && CurSpawnedMiddleIndex > 1)
        {
            return true;
        }

        return false;
    }

    public void RefreshGates()
    {
        foreach (Zone zone in Zones)
        {
            zone.Reset();
        }
    }
}

