#nullable enable
using System;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Entity.Weapons {
    public enum WeaponType { Sword, Bow, Gun, MagicWand, Axe }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ShowIfWeaponTypeAttribute : PropertyAttribute {
        public WeaponType[] WeaponTypes { get; }

        public ShowIfWeaponTypeAttribute(params WeaponType[] weaponTypes) => WeaponTypes = weaponTypes;
    }

    [CreateAssetMenu(fileName = "WeaponData", menuName = "Immersize/WeaponData", order = 1)]
    public class ScriptAbleWeaponData : ScriptableObject {
        #region Fields
        [Header("Weapon Properties")]
            [SerializeField] private string _weaponName = "Unnamed Weapon";
            [SerializeField] private int _levelRequirement = 1;
            [SerializeField] private WeaponType _weaponType = WeaponType.Sword;
            [SerializeField] private float _weight = 1f;
            [SerializeField] private float _damage = 10f;
            [ShowIfWeaponType(WeaponType.Sword, WeaponType.Gun)]
            [SerializeField] private float _durability = 0f;
            
            [ShowIfWeaponType(WeaponType.Sword, WeaponType.Gun)]
            [SerializeField] private float _range = 0f;

            [ShowIfWeaponType(WeaponType.Gun)]
            [SerializeField] private float _fireRate = 0f;  
            [SerializeField] private string? _description;

        [Header("Visuals")]
            public Sprite icon;
            public GameObject modelPrefab;
        #endregion

            private readonly string _pathIcon = "DefaultWeaponIcon";
            private readonly string _pathModel = "DefaultWeaponModel";

            public string WeaponName => _weaponName;
            public int LevelRequirement => _levelRequirement;
            public WeaponType Type => _weaponType;
            public float Weight => _weight;
            public float Damage => _damage;
            public float Durability => _durability;
            public float Range => _range;
            public float FireRate => _fireRate;
            public string? Description => _description;

        private void Init(string name, int level, float weight, float damage, WeaponType type,
            float durability = 0f, float range = 0f, float fireRate = 0f,
            string? description = null, Sprite? icon = null, GameObject? modelPrefab = null) {
            _weaponName = name;
            _levelRequirement = level;
            _weight = weight;
            _damage = damage;
            _weaponType = type;
            _durability = durability;
            _range = range;
            _fireRate = fireRate;
            _description = description;

            this.icon = icon ?? Resources.Load<Sprite>(_pathIcon);
            this.modelPrefab = modelPrefab ?? Resources.Load<GameObject>(_pathModel);
        }
        #region Constructor Methods
            public static ScriptAbleWeaponData CreateSword(string name, int level, float weight, float damage, float durability, string? description = null, Sprite? icon = null, GameObject? model = null) {
                var weapon = CreateInstance<ScriptAbleWeaponData>();
                weapon.Init(name, level, weight, damage, WeaponType.Sword, durability: durability, description: description, icon: icon, modelPrefab: model);
                return weapon;
            }

            public static ScriptAbleWeaponData CreateAxe(string name, int level, float weight, float damage, float durability, string? description = null, Sprite? icon = null, GameObject? model = null) {
                var weapon = CreateInstance<ScriptAbleWeaponData>();
                weapon.Init(name, level, weight, damage, WeaponType.Axe, durability: durability, description: description, icon: icon, modelPrefab: model);
                return weapon;
            }

            public static ScriptAbleWeaponData CreateBow(string name, int level, float weight, float damage, float range, float fireRate, string? description = null, Sprite? icon = null, GameObject? model = null) {
                var weapon = CreateInstance<ScriptAbleWeaponData>();
                weapon.Init(name, level, weight, damage, WeaponType.Bow, range: range, fireRate: fireRate, description: description, icon: icon, modelPrefab: model);
                return weapon;
            }

            public static ScriptAbleWeaponData CreateGun(string name, int level, float weight, float damage, float durability, float range, float fireRate, string? description = null, Sprite? icon = null, GameObject? model = null) {
                var weapon = CreateInstance<ScriptAbleWeaponData>();
                weapon.Init(name, level, weight, damage, WeaponType.Gun, durability: durability, range: range, fireRate: fireRate, description: description, icon: icon, modelPrefab: model);
                return weapon;
            }

            public static ScriptAbleWeaponData CreateMagicWand(string name, int level, float weight, float damage, float fireRate, string? description = null, Sprite? icon = null, GameObject? model = null) {
                var weapon = CreateInstance<ScriptAbleWeaponData>();
                weapon.Init(name, level, weight, damage, WeaponType.MagicWand, fireRate: fireRate, description: description, icon: icon, modelPrefab: model);
                return weapon;
            }
        #endregion
        
        #if UNITY_EDITOR
            [CustomPropertyDrawer(typeof(ShowIfWeaponTypeAttribute))]
            public class ShowIfWeaponTypeDrawer : PropertyDrawer {
                public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)  {
                    SerializedProperty weaponTypeProp = property.serializedObject.FindProperty("_weaponType");
                    if (weaponTypeProp == null) {
                        EditorGUI.PropertyField(position, property, label);
                        return;
                    }

                    WeaponType currentType = (WeaponType)weaponTypeProp.enumValueIndex;
                    ShowIfWeaponTypeAttribute showIf = (ShowIfWeaponTypeAttribute)attribute;

                    if (Array.Exists(showIf.WeaponTypes, wt => wt == currentType)) EditorGUI.PropertyField(position, property, label);
                }

                public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
                    SerializedProperty weaponTypeProp = property.serializedObject.FindProperty("_weaponType");
                    if (weaponTypeProp == null) return EditorGUI.GetPropertyHeight(property, label);

                    WeaponType currentType = (WeaponType)weaponTypeProp.enumValueIndex;
                    ShowIfWeaponTypeAttribute showIf = (ShowIfWeaponTypeAttribute)attribute;

                    if (Array.Exists(showIf.WeaponTypes, wt => wt == currentType)) return EditorGUI.GetPropertyHeight(property, label);
                    else return 0f;
                }
            }
        #endif
    }
}
