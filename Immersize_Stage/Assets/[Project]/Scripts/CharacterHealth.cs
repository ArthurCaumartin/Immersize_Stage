using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    public void TakeDamage(GameObject damageSource, float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }

    public void SetMaxHealth(float value, bool setCurrentHealth = false)
    {
        _maxHealth = value;
        if (setCurrentHealth)
            _currentHealth = _maxHealth;
    }
}