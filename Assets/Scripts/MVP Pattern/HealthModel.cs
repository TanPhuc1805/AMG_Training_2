using UnityEngine;
using UnityEngine.UI;

// Model: Quản lý dữ liệu và logic sức khỏe
public class HealthModel
{
    private float currentHealth;
    private float maxHealth;

    public HealthModel(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth = Mathf.Max(0, currentHealth - damageAmount);
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public float GetHealthPercentage() => currentHealth / maxHealth;
}