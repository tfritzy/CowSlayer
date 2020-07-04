using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Zone
{
    public Zone(Transform zoneGameObject)
    {
        Spawner = zoneGameObject.GetComponent<AreaSpawner>();
        Gate = zoneGameObject.transform.Find("Wall").Find("Gate").gameObject;
    }

    public AreaSpawner Spawner;
    public GameObject Gate;
}

