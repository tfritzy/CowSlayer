using System;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static class MapParameters
    {
        public const float BlockYPos = -1f;
    }

    public static class FilePaths
    {
        public const string Maps = "Assets/Resources/Maps";
        public const string TilePrefabLocation = "Prefabs/GridTiles";
    }

    public static class Layers {
        public const int Character = 8;
    }

    public static class GameObjects
    {
        private static Dictionary<TileType, GameObject> _blocks;
        public static Dictionary<TileType, GameObject> Blocks
        {
            get
            {
                if (_blocks == null)
                {
                    _blocks = new Dictionary<TileType, GameObject>();
                    foreach (TileType gridType in Enum.GetValues(typeof(TileType)))
                    {
                        Debug.Log($"{Constants.FilePaths.TilePrefabLocation}/{gridType}");
                        Blocks[gridType] = Resources.Load<GameObject>($"{Constants.FilePaths.TilePrefabLocation}/{gridType}");
                    }
                }
                return _blocks;
            }
        }

        private static Dictionary<ObjectType, GameObject> _objects;
        public static Dictionary<ObjectType, GameObject> Objects
        {
            get
            {
                if (_objects == null)
                {
                    _objects = new Dictionary<ObjectType, GameObject>();
                    // foreach (ObjectType gridType in Enum.GetValues(typeof(ObjectType)))
                    // {
                    //     Debug.Log($"{Constants.FilePaths.TilePrefabLocation}/{gridType}");
                    //     Blocks[gridType] = Resources.Load<GameObject>($"{Constants.FilePaths.TilePrefabLocation}/{gridType}");
                    // }
                }
                return _objects;
            }
        }
    }
}