using NUnit.Framework.Interfaces;
using UnityEngine;


namespace Entity.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Immersize/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [Header("Character Properties")]
        [SerializeField] protected string _characterName;
        [SerializeField] protected string _description;
        [SerializeField] protected int _maxHealth;
        [SerializeField] protected float _moveSpeed;

        public string Name { get => _characterName; }
        public string Description { get => _description; }
        public int MaxHealth { get => _maxHealth; }
        public float MoveSpeed { get => _moveSpeed; }
        

        [Header("Visual : ")]
        public Sprite icon;
        public GameObject meshPrefab;

        private readonly string _pathIcon = "DefaultCharacterIcon";
        private readonly string _pathMeshInstance = "DefaultCharacterModel";

        public CharacterData(string name, int maxHealth, float moveSpeed
        , string description = null, Sprite icon = null, GameObject meshPrefab = null)
        {
            _characterName = name;
            _description = description;
            _maxHealth = maxHealth;
            _moveSpeed = moveSpeed;

            this.icon = icon ?? Resources.Load<Sprite>(_pathIcon);
            this.meshPrefab = meshPrefab ?? Resources.Load<GameObject>(_pathMeshInstance);
        }
    }
}
