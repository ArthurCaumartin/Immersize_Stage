using System;
using UnityEngine;
using Entity;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[Serializable]
public abstract class LivingEntity : EntityModel
{
    [Space]
    [SerializeField] protected int _maxHealth = 100;
    protected float _currentHealth = 50;
    [SerializeField] protected float _movementSpeed = 5;

    public int MaxHealth { get => _maxHealth; }
    public float CurrentHealth { get => _currentHealth; }

    protected LivingEntity(string name) : base(name) { }

    public abstract void Kill();
    public abstract void TakeDamage(Weapon damageSource, float damageQuantity);
}

