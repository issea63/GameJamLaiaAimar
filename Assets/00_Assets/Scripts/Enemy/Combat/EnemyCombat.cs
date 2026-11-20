using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    private EnemyCombatStats enemyCombatStats;
    private EnemyController enemyController;
    private NavMeshAgent agent;
    private Animator animator;

    public AudioSource attackSound;

    [SerializeField] private float attackAnimationDuration = 1f;

    private Coroutine attackRoutine;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyCombatStats = GetComponent<EnemyCombatStats>();
        enemyController = GetComponent<EnemyController>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (enemyCombatStats.cooldown > 0)
        {
            enemyCombatStats.SetStat(
                ref enemyCombatStats.cooldown,
                -Time.deltaTime
            );
        }
        else if (enemyController.playerDetected &&
                 !enemyController.isAttacking)
        {
            AttackPlayer();
        }
    }

    public void AttackPlayer()
    {
        if (enemyCombatStats.cooldown > 0 ||
            enemyController.isAttacking)
        {
            return;
        }

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            enemyCombatStats.attackRange,
            enemyController.playerLayer
        );

        if (hits.Length == 0)
        {
            return;
        }

        PlayerHealth playerHealth =
            hits[0].GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
        {
            return;
        }

        enemyController.isAttacking = true;
        enemyController.StopWalkSound();

        agent.isStopped = true;

        playerHealth.TakeDamage(enemyCombatStats.damage);
        enemyCombatStats.SetStat(ref enemyCombatStats.cooldown, 3f);

        if (attackSound != null)
        {
            attackSound.Play();
        }
        else
        {
            Debug.LogWarning("No attack sound assigned to the enemy.");
        }

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }

        attackRoutine = StartCoroutine(EndAttackAfterDelay());
    }

    private IEnumerator EndAttackAfterDelay()
    {
        yield return new WaitForSeconds(attackAnimationDuration);

        enemyController.isAttacking = false;
        agent.isStopped = false;
        attackRoutine = null;
    }
}
