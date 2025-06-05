#nullable enable
using System;
using UnityEngine;


namespace Entity {
    [Serializable]
    public abstract class EntityModel {
        [SerializeField] protected internal string name;
        [SerializeField] protected internal string? description;

        [SerializeField] protected internal float? rarityType;
        protected EntityModel(string name) {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public Rarity Rarity => RarityExtensions.Instance.GetByName(rarityType.ToString()); 

    }
}
