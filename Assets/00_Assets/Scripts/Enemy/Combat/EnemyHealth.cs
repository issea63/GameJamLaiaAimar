using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Stats
{
    public float maxHealth = 100f;
    [SerializeField]
    float currentHealth;
    EnemyHealthUI healthUI;
    private void Start()
    {
        healthUI = GetComponent<EnemyHealthUI>();
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
    private void Die()
    {
        Destroy(gameObject);
    }
}
