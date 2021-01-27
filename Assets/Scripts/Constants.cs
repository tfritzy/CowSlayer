using System;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public static class Constants
{
    public static class UI
    {
        public static class Colors
        {
            public readonly static Color VeryBrightBase = ColorExtensions.Create("a66790");
            public readonly static Color BrightBase = ColorExtensions.Create("74355E");
            public readonly static Color Base = ColorExtensions.Create("3F2C45");
            public readonly static Color LightBase = ColorExtensions.Create("4E2E51");
            public readonly static Color HighLight = ColorExtensions.Create("ECB000");
        }
    }

    private static Dictionary<SkillType, Skill> _skills;
    public static Dictionary<SkillType, Skill> Skills
    {
        get
        {
            if (_skills == null)
            {
                _skills = new Dictionary<SkillType, Skill>()
                {
                    {
                        SkillType.Firebolt,
                        new FireBolt()
                    },
                    {
                        SkillType.Fireball,
                        new FireBall()
                    },
                    {
                        SkillType.Meteor,
                        new Meteor()
                    },
                    {
                        SkillType.Attunement,
                        new Attunement()
                    },
                    {
                        SkillType.Whack,
                        new Whack()
                    },
                    {
                        SkillType.FlameSprite,
                        new FlameSprite()
                    },
                    {
                        SkillType.FireWave,
                        new FireWave()
                    },
                    {
                        SkillType.Charge,
                        new Charge()
                    },
                    {
                        SkillType.CrossbowAttack,
                        new CrossbowAttack()
                    },
                    {
                        SkillType.Punch,
                        new Punch()
                    },
                    {
                        SkillType.SwordSwing,
                        new SwordSwing()
                    }
                };

                foreach (SkillType type in Enum.GetValues(typeof(SkillType)))
                {
                    if (_skills.ContainsKey(type) == false)
                    {
                        throw new Exception($"You forgot to add the skill {type} to Constants.Skills");
                    }
                }
            }

            return _skills;
        }
    }

    public static class WorldProperties
    {
        public const float GroundLevel = 0f;
        public const int PlayerAbilityCount = 2;
    }

    public static class FilePaths
    {
        public const string Icons = "Icons";

        public class Prefabs
        {
            public const string UI = "Prefabs/Objects/UI";
            public const string Drops = "Prefabs/Objects/Items/Drops";
            public const string Equipment = "Prefabs/Objects/Items/Equipment";
            public const string Cows = "Prefabs/Objects/Cows";
            public const string Skills = "Prefabs/Objects/Skills";
            public const string Decals = "Prefabs/Objects/Decals";
            public const string Effects = "Prefabs/Effects";
        }
        public const string AreaSpawns = "AreaSpawns";
    }

    public static class Layers
    {
        public const int Character = 1 << 8;
        public const int Ground = 1 << 9;
        public const int Drops = 1 << 10;
    }

    public static class Tags
    {
        public const string Player = "Player";
        public const string InteractableUI = "InteractableUI";
        public const string Ground = "Ground";
    }

    private static Dictionary<string, Sprite> _icons;
    public static Dictionary<string, Sprite> Icons
    {
        get
        {
            if (_icons == null)
            {
                _icons = new Dictionary<string, Sprite>();
                Sprite[] sprites = Resources.LoadAll<Sprite>(FilePaths.Icons);
                foreach (Sprite sprite in sprites)
                {
                    _icons[sprite.name] = sprite;
                }
            }

            return _icons;
        }
    }

    public static class Prefabs
    {
        public static class Decals
        {
            private static GameObject _arc;
            public static GameObject Arc
            {
                get
                {
                    if (_arc == null)
                    {
                        _arc = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Decals}/Arc");
                    }

                    return _arc;
                }
            }

            private static List<GameObject> _bloodSplats;
            public static GameObject BloodSplat
            {
                get
                {
                    if (_bloodSplats == null)
                    {
                        _bloodSplats = new List<GameObject>();
                        for (int i = 0; i < 3; i++)
                        {
                            _bloodSplats.Add(Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Decals}/BloodSplat" + i));
                        }
                    }

                    return _bloodSplats[UnityEngine.Random.Range(0, _bloodSplats.Count)];
                }
            }

            private static GameObject _longRectangle;
            public static GameObject LongRectangle
            {
                get
                {
                    if (_longRectangle == null)
                    {
                        _longRectangle = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Decals}/LongRectangle");
                    }

                    return _longRectangle;
                }
            }
        }

        public static class Effects
        {
            private static GameObject _bloodExplosion;
            public static GameObject BloodExplosion
            {
                get
                {
                    if (_bloodExplosion == null)
                    {
                        _bloodExplosion = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Effects}/BloodExplosion");
                    }
                    return _bloodExplosion;
                }
            }

            private static GameObject _cowBodyParts;
            public static GameObject CowBodyParts
            {
                get
                {
                    if (_cowBodyParts == null)
                    {
                        _cowBodyParts = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Effects}/CowParts");
                    }
                    return _cowBodyParts;
                }
            }
        }

        private static GameObject _onScreenNumber;
        public static GameObject OnScreenNumber
        {
            get
            {
                if (_onScreenNumber == null)
                {
                    _onScreenNumber = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/OnScreenNumber");
                }

                return _onScreenNumber;
            }
        }

        private static GameObject _damageNumber;
        public static GameObject DamageNumber
        {
            get
            {
                if (_damageNumber == null)
                {
                    _damageNumber = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/DamageNumber");
                }

                return _damageNumber;
            }
        }

        private static GameObject _closeMenuButton;
        public static GameObject CloseMenuButton
        {
            get
            {
                if (_closeMenuButton == null)
                {
                    _closeMenuButton = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/CloseMenuButton");
                }
                return _closeMenuButton;
            }
        }

        private static GameObject _purchaseItemMenu;
        public static GameObject PurchaseItemMenu
        {
            get
            {
                if (_purchaseItemMenu == null)
                {
                    _purchaseItemMenu = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/PurchaseItemMenu");
                }
                return _purchaseItemMenu;
            }
        }

        private static GameObject _itemDetailsPage;
        public static GameObject ItemDetailsPage
        {
            get
            {
                if (_itemDetailsPage == null)
                {
                    _itemDetailsPage = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/ItemDetailsPage");
                }

                return _itemDetailsPage;
            }
        }


        private static GameObject _emptyDrop;
        public static GameObject EmptyDrop
        {
            get
            {
                if (_emptyDrop == null)
                {
                    _emptyDrop = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Equipment}/Drop");
                }

                return _emptyDrop;
            }
        }

        private static GameObject _deathScreenUI;
        public static GameObject DeathScreenUI
        {
            get
            {
                if (_deathScreenUI == null)
                {
                    _deathScreenUI = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/DeathScreen");
                }
                return _deathScreenUI;
            }
        }

        private static Dictionary<DropType, GameObject> _drops;
        public static Dictionary<DropType, GameObject> Drops
        {
            get
            {
                if (_drops == null)
                {
                    _drops = new Dictionary<DropType, GameObject>();

                    foreach (DropType dropType in Enum.GetValues(typeof(DropType)))
                    {
                        _drops[dropType] = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Drops}/{dropType}");
                    }
                }

                return _drops;
            }
        }

        private static GameObject _playerStatsWindow;
        public static GameObject PlayerStatsWindow
        {
            get
            {
                if (_playerStatsWindow == null)
                {
                    _playerStatsWindow = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.UI}/PlayerStats");
                }
                return _playerStatsWindow;
            }
        }

        private static Dictionary<CowType, GameObject> _cowPrefabs;
        public static Dictionary<CowType, GameObject> CowPrefabs
        {
            get
            {
                if (_cowPrefabs == null)
                {
                    _cowPrefabs = new Dictionary<CowType, GameObject>();
                    foreach (CowType cowType in Enum.GetValues(typeof(CowType)))
                    {
                        _cowPrefabs.Add(cowType, Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Cows}/{cowType}"));
                    }
                }
                return _cowPrefabs;
            }
        }

        private static GameObject _groundFire;
        public static GameObject GroundFire
        {
            get
            {
                if (_groundFire == null)
                {
                    _groundFire = Resources.Load<GameObject>($"{Constants.FilePaths.Prefabs.Skills}/GroundFire");
                }

                return _groundFire;
            }
        }
    }

    public static class Persistant
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

        private static List<AbilityButton> _abilityButtons;
        public static List<AbilityButton> AbilityButtons
        {
            get
            {
                if (_abilityButtons == null || _abilityButtons.Count == 0)
                {
                    _abilityButtons = new List<AbilityButton>();
                    for (int i = 0; i < WorldProperties.PlayerAbilityCount; i++)
                    {
                        _abilityButtons.Add(Constants.Persistant.InteractableUI.Find($"Skill{i}").GetComponent<AbilityButton>());
                    }
                }

                return _abilityButtons;
            }
        }

        private static XPBar _xpBar;
        public static XPBar XPBar
        {
            get
            {
                if (_xpBar == null)
                {
                    _xpBar = Constants.Persistant.InteractableUI.transform.Find("XPBar").GetComponent<XPBar>();
                }

                return _xpBar;
            }
        }

        private static XPBar _manaBall;
        public static XPBar ManaBall
        {
            get
            {
                if (_manaBall == null)
                {
                    _manaBall = Constants.Persistant.InteractableUI.transform.Find("ManaResourceGlobe").GetComponent<XPBar>();
                }

                return _manaBall;
            }
        }

        private static XPBar _healthBall;
        public static XPBar HealthBall
        {
            get
            {
                if (_healthBall == null)
                {
                    _healthBall = Constants.Persistant.InteractableUI.transform.Find("HealthResourceGlobe").GetComponent<XPBar>();
                }

                return _healthBall;
            }
        }

        public static Player PlayerScript
        {
            get
            {
                return Player.GetComponent<Player>();
            }
        }

        private static Vector3 _spawnPoint;
        public static Vector3 SpawnPoint
        {
            get
            {
                if (_spawnPoint == null)
                {
                    _spawnPoint = GameObject.Find("SpawnPoint").transform.position;
                }
                return _spawnPoint;
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
        public static Camera Camera
        {
            get
            {
                if (_camera == null)
                {
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

        private static ZoneManager _zoneManager;
        public static ZoneManager ZoneManager
        {
            get
            {
                if (_zoneManager == null)
                {
                    _zoneManager = GameObject.Find("Zones").GetComponent<ZoneManager>();
                }
                return _zoneManager;
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

        private static Joystick _joystick;
        public static Joystick Joystick
        {
            get
            {
                if (_joystick == null)
                {
                    _joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
                }
                return _joystick;
            }
        }
    }
}