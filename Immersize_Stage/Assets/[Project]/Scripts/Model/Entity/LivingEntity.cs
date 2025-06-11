using System;
using UnityEngine;
using Entity;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[Serializable]
public abstract class LivingEntity : EntityModel
{
    [Space]
    [SerializeField] protected int maxHealth = 100;
    protected float currentHealth = 50;
    [SerializeField] protected float movementSpeed = 5;
    [SerializeField] protected float attackSpeed = 5;

    public float AttackSpeed { get => attackSpeed; }
    public int MaxHealth { get => maxHealth; }
    public float CurrentHealth { get => currentHealth; }
    public float HealthRatio { get => currentHealth / maxHealth; }

    protected LivingEntity(string name) : base(name) { }

    public abstract void Attack();
    public abstract void TakeDamage(float damageQuantity);
    public abstract void Kill();
}

