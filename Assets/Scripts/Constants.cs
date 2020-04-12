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
        public const string UIPrefabs = "Prefabs/Objects/UI";
    }

    public static class Layers {
        public const int Character = 1 << 8;
    }

    public static class Tags {
        public const string Player = "Player";
    }

    public static class GameObjects
    {
        public static Camera _camera;
        public static Camera Camera {
            get {
                if (_camera == null){
                    _camera = Camera.main;
                }

                return _camera;
            }
        }
        
        private static Transform _healthUIParent;
        public static Transform HealthUIParent
        {
            get 
            {
                if (_healthUIParent == null)
                {
                    _healthUIParent = GameObject.Find("HealthUI").transform;
                }
                return _healthUIParent;
            }
        }

        private static Transform _damageUIParent;
        public static Transform DamageUIParent
        {
            get
            {
                if (_damageUIParent == null)
                {
                    _damageUIParent = GameObject.Find("DamageNumbers").transform;
                }
                return _damageUIParent;
            }
        }
    }
}