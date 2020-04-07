using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public MapData MapMap;

    void Start()
    {
        MapLoader.Load("Grasslands");
    }

    public static Vector3 GridPosToWorldPos(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x, gridPos.y, Constants.MapParameters.BlockZPos);
    }
}
