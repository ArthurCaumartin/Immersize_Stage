using Entity.Character;
using UnityEngine;

public class Character : LivingEntity
{
    [SerializeField] protected CharacterUI characterUI;
    [SerializeField] protected CharacterAnimation characterAnimation;

    public Character(string name) : base(name) { }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Kill()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(float damageQuantity)
    {
        characterAnimation.PlayState(CharacterAnimation.STATE_TAKE_DAMAGE);
        currentHealth -= damageQuantity;
        characterUI.SetHealthBarValue(HealthRatio);
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public virtual void BakeCharacter(CharacterData data)
    {
        name = data.Name;
        maxHealth = data.MaxHealth;
        currentHealth = maxHealth;
    }
}