using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    EnemyCombatStats enemyCombatStats;
    EnemyController enemyController;
    public AudioSource attackSound;
    NavMeshAgent agent;
    Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyCombatStats = GetComponent<EnemyCombatStats>();
        enemyController = GetComponent<EnemyController>();
        agent = GetComponent<NavMeshAgent>();
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
        Collider[] hits = Physics.OverlapSphere(transform.position, enemyCombatStats.attackRange, enemyController.playerLayer);

        if (enemyCombatStats.cooldown > 0 || hits.Length == 0)
        {
            agent.isStopped = false;
            enemyController.DetectPlayer();
            return;
        }

        PlayerHealth playerHealth = hits[0].GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
        {
            agent.isStopped = false;
            enemyController.DetectPlayer();
            return;
        }

        playerHealth.TakeDamage(enemyCombatStats.damage);
        enemyCombatStats.SetStat(ref enemyCombatStats.cooldown, 3f);
        agent.isStopped = true;
        

        if (attackSound != null)
            attackSound.Play();
        else
            Debug.LogWarning("No attack sound assigned to the enemy.");
        if(animator != null)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            Debug.LogWarning("Animator not assigned in EnemyCombat.");
        }
    }
}
