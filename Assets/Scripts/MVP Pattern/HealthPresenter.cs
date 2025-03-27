using UnityEngine;

public class HealthPresenter
{
    private HealthModel healthModel;
    private HealthView healthView;

    public HealthPresenter(HealthModel model, HealthView view)
    {
        healthModel = model;
        healthView = view;
    }

    public void TakeDamage(float damageAmount)
    {
        healthModel.TakeDamage(damageAmount);
        UpdateHealthBar();
    }

    public void Heal(float healAmount)
    {
        healthModel.Heal(healAmount);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthView.UpdateHealthBar(healthModel.GetHealthPercentage());
    }
}
