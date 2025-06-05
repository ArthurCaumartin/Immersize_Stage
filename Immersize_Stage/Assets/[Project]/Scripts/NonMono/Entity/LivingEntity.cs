using System;
using UnityEngine;

[Serializable]
public abstract class LivingEntity : Entity
{
    [Space]
    [SerializeField] protected int _maxHealth = 100; // Remplacer les components de health par autre chose ? (je sais pas quoi  exactement)
    protected float _currentHealth = 50;
    [SerializeField] protected float _movementSpeed = 5;


    public abstract void TakeDamage(Entity damageSource, float damageQuantity);
    public abstract void Kill();
}

