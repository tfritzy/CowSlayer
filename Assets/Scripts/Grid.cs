using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public TileData[,] Map;

    private static Vector2Int MapSize = new Vector2Int(Constants.MapXSize, Constants.MapYSize);
    private Dictionary<TileType, GameObject> Blocks;
    private const string TilePrefabLocation = "Prefabs/GridTiles";
    private Transform BlockParent;

    void Start()
    {
        LoadTileGameObjects();
        PlaceTiles();
    }

    private void LoadTileGameObjects()
    {
        Blocks = new Dictionary<TileType, GameObject>();
        foreach (TileType gridType in Enum.GetValues(typeof(TileType)))
        {
            Debug.Log($"{TilePrefabLocation}/{gridType}");
            Blocks[gridType] = Resources.Load<GameObject>($"{TilePrefabLocation}/{gridType}");
        }
    }

    private void PlaceTiles()
    {
        this.Map = new TileData[Constants.MapXSize, Constants.MapYSize];
        this.BlockParent = Instantiate(new GameObject()).transform;
        this.BlockParent.name = "Blocks";
        bool isDarkBlock = false;
        for (int x = 0; x < MapSize.x; x++)
        {
            for (int y = 0; y < MapSize.y; y++)
            {
                PlaceBlock(new Vector2Int(x, y), TileType.Grass, isDarkBlock);
                isDarkBlock = !isDarkBlock;
            }
            isDarkBlock = !isDarkBlock;
        }
    }

    private void PlaceBlock(Vector2Int gridPos, TileType type, bool isDarkBlock)
    {
        GameObject inst = Instantiate(Blocks[type], GridPosToWorldPos(gridPos), new Quaternion(), this.BlockParent);
        if (isDarkBlock)
        {
            Color color = inst.GetComponent<MeshRenderer>().material.color;
            inst.GetComponent<MeshRenderer>().material.color = DarkenColor(color);
        }

        this.Map[gridPos.x, gridPos.y] = TileData.TileDataMap[type];
    }

    public static Vector3 GridPosToWorldPos(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x, gridPos.y, Constants.BlockZPos);
    }

    private Color DarkenColor(Color color)
    {
        color = color * .95f;
        color.a = 1;
        return color;
    }

}
