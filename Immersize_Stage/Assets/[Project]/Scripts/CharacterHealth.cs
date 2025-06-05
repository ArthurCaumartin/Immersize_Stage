using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    [HideInInspector] public UnityEvent<float> OnHealthChange;

    public void TakeDamage(GameObject damageSource, float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }
        OnHealthChange.Invoke(HealthRatio());
    }

    public void SetMaxHealth(float value, bool setCurrentHealth = false)
    {
        _maxHealth = value;
        if (setCurrentHealth)
        {
            _currentHealth = _maxHealth;
            OnHealthChange.Invoke(1);
        }
    }

    public float HealthRatio() => _currentHealth / _maxHealth;
}