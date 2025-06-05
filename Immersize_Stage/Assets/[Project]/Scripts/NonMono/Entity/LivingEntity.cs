using System;

using UnityEngine;
using Entity;

[RequireComponent(typeof(Rigidbody))]
[Serializable]
public abstract class LivingEntity : EntityModel {


    [Space]
    [SerializeField] protected int _maxHealth = 100; // Remplacer les components de health par autre chose ? (je sais pas quoi  exactement)
    protected float _currentHealth = 50;
    [SerializeField] protected float _movementSpeed = 5;

    protected LivingEntity(string name) : base(name) { }

    public abstract void TakeDamage(EntityModel damageSource, float damageQuantity);
    public abstract void Kill();

    internal void TakeDamage(Sword sword, float damageQuantity)
    {
        throw new NotImplementedException();
    }
}

