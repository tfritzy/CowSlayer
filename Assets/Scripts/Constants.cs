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
        public const string Icons = "Icons";
        public const string UIPrefabs = "Prefabs/Objects/UI";
        public const string AreaSpawns = "AreaSpawns";
    }

    public static class Layers {
        public const int Character = 1 << 8;
    }

    public static class Tags {
        public const string Player = "Player";
        public const string InteractableUI = "InteractableUI";
    }

    public static class GameObjects
    {
        private static GameObject _player;
        public static GameObject Player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.Find("Player");
                }
                return _player;
            }
        }

        public static Player PlayerScript {
            get {
                return Player.GetComponent<Player>();
            }
        }


        private static GameObject _canvas;
        public static GameObject Canvas
        {
            get
            {
                if (_canvas == null)
                {
                    _canvas = GameObject.Find("Canvas");
                }
                return _canvas;
            }
        }

        private static GameObject _interactableCanvas;
        public static GameObject InteractableCanvas
        {
            get
            {
                if (_interactableCanvas == null)
                {
                    _interactableCanvas = GameObject.Find("InteractableUI");
                }
                return _interactableCanvas;
            }
        }

        private static Camera _camera;
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

        private static Transform _interactableUI;
        public static Transform InteractableUI
        {
            get 
            {
                if (_interactableUI == null)
                {
                    _interactableUI = GameObject.Find("InteractableUI").transform;
                }
                return _interactableUI;
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

        private static Transform _cowParent;
        public static Transform CowParent
        {
            get
            {
                if (_cowParent == null)
                {
                    _cowParent = GameObject.Find("Cows").transform;
                }
                return _cowParent;
            }
        }
    }
}