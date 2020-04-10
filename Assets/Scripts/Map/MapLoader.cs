using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class MapLoader
{
    public static MapData Load(string mapName)
    {
        MapData mapData = ReadMapFile(mapName);
        PlaceGroundBlocks(mapData);
        return mapData;
    }

    public static void Save(MapData mapData)
    {
        SaveMapToFile(mapData);
    }

    private static void PlaceGroundBlocks(MapData mapData)
    {
        GameObject groundParent = GameObject.Instantiate(new GameObject());
        groundParent.name = "Ground";
        bool isDark = false;
        for (int x = 0; x < mapData.GroundTiles.GetLength(0); x++)
        {
            for (int z = 0; z < mapData.GroundTiles.GetLength(1); z++)
            {
                PlaceBlock(new Vector2Int(x, z), mapData.GroundTiles[x, z], groundParent.transform, isDark);
                isDark = !isDark;
            }

            if (mapData.GroundTiles.GetLength(1) % 2 == 0)
            {
                isDark = !isDark;
            }
        }
    }

    private static void PlaceObjects(MapData mapData){
        GameObject objectsParent = GameObject.Instantiate(new GameObject());
        objectsParent.name = "Objects";
        for (int x = 0; x < mapData.Objects.GetLength(0); x++)
        {
            for (int z = 0; z < mapData.Objects.GetLength(1); z++)
            {
                PlaceObject(new Vector2Int(x, z), mapData.Objects[x, z], objectsParent.transform);
            }
        }
    }

    private static void PlaceObject(Vector2Int gridPos, ObjectType type, Transform parent){
        // GameObject inst = GameObject.Instantiate(
        //     Constants.GameObjects.Blocks[type],
        //     Map.GridPosToWorldPos(gridPos),
        //     new Quaternion(),
        //     parent);
    }

    private static void PlaceBlock(Vector2Int gridPos, TileType type, Transform parent, bool isDarkBlock)
    {
        GameObject inst = GameObject.Instantiate(
            Constants.GameObjects.Blocks[type],
            Map.GridPosToWorldPos(gridPos),
            new Quaternion(),
            parent);
        if (isDarkBlock)
        {
            Color color = inst.GetComponent<MeshRenderer>().material.color;
            inst.GetComponent<MeshRenderer>().material.color = DarkenColor(color);
        }
    }

    private static Color DarkenColor(Color color)
    {
        color = color * .95f;
        color.a = 1;
        return color;
    }

    public static MapData ReadMapFile(string mapName)
    {
        string path = $"{Constants.FilePaths.Maps}/{mapName}.json";
        StreamReader reader = new StreamReader(path);
        string jsonMap = reader.ReadToEnd();
        reader.Close();
        return JsonConvert.DeserializeObject<MapData>(jsonMap);
    }

    public static void SaveMapToFile(MapData mapData)
    {
        string path = $"{Constants.FilePaths.Maps}/{mapData.Name}.json";
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(JsonConvert.SerializeObject(mapData));
        writer.Close();
    }
}