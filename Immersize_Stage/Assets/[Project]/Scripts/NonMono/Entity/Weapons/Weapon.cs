using UnityEngine;

using Entity.Weapons;
using Entity;
public abstract class Weapon : NonLivingEntity {
    // [SerializeField] protected float _damage;
    // [SerializeField] protected float _attackSpeed;
    // [SerializeField] private float _attackRange;

    // [SerializeField] private GameObject _projectile;

    public EntityModel Owner { get; private set; }
    public ScriptAbleWeaponData Data { get; private set; }

    protected Weapon(string name) : base(name) { }

    protected abstract void Attack(LivingEntity damageSource, float damageQuantity);
}

