using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : Stats
{
    public float maxHealth = 100f;
    [SerializeField]
    float currentHealth;
    EnemyHealthUI healthUI;
    Animator animator;
    public AudioSource enemyHitSound;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        healthUI = GetComponent<EnemyHealthUI>();
        SetStat(ref currentHealth, maxHealth);
        healthUI.UpdateHealthBar(currentHealth, maxHealth);
    }
    public void TakeDamage(float damage)
    {
        SetStat(ref currentHealth, -damage);
        healthUI.UpdateHealthBar(currentHealth, maxHealth);
        enemyHitSound.Play();
        EnemyHitGlow hitGlow = GetComponent<EnemyHitGlow>();
        hitGlow.GlowHit();
        if(currentHealth <= 0)
        {
            Die();   
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    public IEnumerator DieRoutine()
    {
        if(animator != null)
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("Die");
        }
        else
        {
            Debug.LogWarning("Animator not assigned in EnemyHealth.");
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
