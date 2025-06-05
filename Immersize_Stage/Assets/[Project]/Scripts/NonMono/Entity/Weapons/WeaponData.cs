#nullable enable
using System;
using UnityEngine;

namespace Entity.Weapons {

    public enum WeaponType { Sword, Bow, Gun, MagicWand, Axe }
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Immersize/WeaponData", order = 1)]
    public class ScriptAbleWeaponData : ScriptableObject {
        [Header("Weapon Properties")]
            [SerializeField] private string _weaponName;
            [SerializeField] protected int _levelRequirement;
            [SerializeField] protected float _weight;
            [SerializeField] protected float _damage;
            [SerializeField] protected float _durability;
            [SerializeField] protected float _range;
            [SerializeField] protected float _fireRate;
            [SerializeField] protected string? _description;
            [SerializeField] protected WeaponType _weaponType;

        [Header("Visuals")]
            public Sprite icon;
            public GameObject modelPrefab;

            private readonly string _pathIcon = "DefaultWeaponIcon";
            private readonly string _pathModel = "DefaultWeaponModel";

            public ScriptAbleWeaponData(string weaponName, int levelRequirement, float weight, float damage, 
                float durability, float range, float fireRate
                , string? description = null, WeaponType weaponType = WeaponType.Sword,
                Sprite? icon = null, GameObject? modelPrefab = null) {
                this._weaponName = weaponName;
                _levelRequirement = levelRequirement;
                _weight = weight;
                _damage = damage;
                _durability = durability;
                _range = range;
                _fireRate = fireRate;
                _description = description;
                _weaponType = weaponType;

                this.icon = icon ?? Resources.Load<Sprite>(_pathIcon);
                this.modelPrefab = modelPrefab ?? Resources.Load<GameObject>(_pathModel);
            }

    }

}