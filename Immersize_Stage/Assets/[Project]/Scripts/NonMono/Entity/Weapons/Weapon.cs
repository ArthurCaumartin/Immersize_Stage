using UnityEngine;

public abstract class Weapon : NonLivingEntity
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] private float _attackRange;

    [SerializeField] private GameObject _projectile;


    protected abstract void Attack(LivingEntity damageSource, float damageQuantity);
}

