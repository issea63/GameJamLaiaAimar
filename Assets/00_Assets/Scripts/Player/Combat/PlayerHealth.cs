using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Stats
{
    public float maxHealth = 100f;
    [SerializeField]
    float currentHealth;
    PlayerHealthUI healthUI;

    private void Start()
    {
        healthUI = GetComponent<PlayerHealthUI>();
        SetStat(ref currentHealth, maxHealth);
        healthUI.UpdateHealthBar(currentHealth, maxHealth);
    }
    public void TakeDamage(float damage)
    {
        SetStat(ref currentHealth, -damage);
        healthUI.UpdateHealthBar(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float healAmount)
    {
        SetStat(ref currentHealth, healAmount);
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthUI.UpdateHealthBar(currentHealth, maxHealth);

    }
    public void Die()
    {
        SceneManager.LoadScene("Lose");
    }
}