using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    EnemyCombatStats enemyCombatStats;
    EnemyController enemyController;
    public AudioSource attackSound;
    private void Start()
    {
        enemyCombatStats = GetComponent<EnemyCombatStats>();
        enemyController = GetComponent<EnemyController>();
    }
    private void Update()
    {
        if(enemyCombatStats.cooldown > 0)
        {
            enemyCombatStats.SetStat(ref enemyCombatStats.cooldown, -Time.deltaTime);
        }
        else if(enemyCombatStats.cooldown <= 0 && enemyController.playerDetected)
        {
            AttackPlayer();
        }
    }
    public void AttackPlayer()
    {
        if(enemyCombatStats.cooldown <= 0)
        {
            PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
            playerHealth.TakeDamage(enemyCombatStats.damage);
            enemyCombatStats.ResetStat(ref enemyCombatStats.cooldown, 1f);
            if(attackSound != null)
            {
                attackSound.Play();
            }
            else return;
        }
    }
}
