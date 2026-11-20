using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float detectionRange = 5f;
    public LayerMask playerLayer;
    public bool playerDetected;
    public bool isAttacking;
    public Transform[] patrolPoints;
    public AudioSource walkSound;

    [SerializeField] private int patrolIndex;

    private NavMeshAgent agent;
    private EnemyCombatStats enemyCombatStats;
    private Coroutine walkSoundCoroutine;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyCombatStats = GetComponent<EnemyCombatStats>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        if (isAttacking)
        {
            StopWalkSound();
            return;
        }

        DetectPlayer();
    }

    public void DetectPlayer()
    {
        if (isAttacking)
        {
            StopWalkSound();
            return;
        }

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            detectionRange,
            playerLayer
        );

        if (hits.Length == 0)
        {
            playerDetected = false;
            agent.isStopped = false;
            Patrol();
            return;
        }

        playerDetected = true;
        Transform playerTransform = hits[0].transform;

        bool playerInAttackRange = Physics.OverlapSphere(
            transform.position,
            enemyCombatStats.attackRange,
            playerLayer
        ).Length > 0;

        if (playerInAttackRange)
        {
            // No caminar ni reproducir el sonido dentro del rango de ataque.
            StopWalkSound();
            agent.isStopped = true;

            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
            PlayWalkSound();

            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
        }
    }

    private void Patrol()
    {
        if (isAttacking || patrolPoints == null || patrolPoints.Length == 0)
        {
            StopWalkSound();
            return;
        }

        agent.isStopped = false;

        if (Vector3.Distance(
                transform.position,
                patrolPoints[patrolIndex].position
            ) < agent.stoppingDistance)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        agent.SetDestination(patrolPoints[patrolIndex].position);
        PlayWalkSound();

        if (animator != null)
        {
            animator.SetBool("isWalking", true);
        }
    }

    private void PlayWalkSound()
    {
        if (isAttacking ||
            agent.isStopped ||
            walkSound == null ||
            IsPlayerInAttackRange())
        {
            StopWalkSound();
            return;
        }

        if (walkSoundCoroutine == null)
        {
            walkSoundCoroutine = StartCoroutine(PlayWalkSoundCoroutine());
        }
    }

    private bool IsPlayerInAttackRange()
    {
        if (enemyCombatStats == null)
        {
            return false;
        }

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            enemyCombatStats.attackRange,
            playerLayer
        );

        return hits.Length > 0;
    }

    public void StopWalkSound()
    {
        if (walkSoundCoroutine != null)
        {
            StopCoroutine(walkSoundCoroutine);
            walkSoundCoroutine = null;
        }

        if (walkSound != null)
        {
            walkSound.Stop();
        }
    }

    private IEnumerator PlayWalkSoundCoroutine()
    {
        if (walkSound == null || isAttacking || IsPlayerInAttackRange())
        {
            walkSoundCoroutine = null;
            yield break;
        }

        walkSound.Play();

        while (walkSound != null &&
               walkSound.isPlaying &&
               !isAttacking &&
               !IsPlayerInAttackRange())
        {
            yield return null;
        }

        if (walkSound != null &&
            (isAttacking || IsPlayerInAttackRange()))
        {
            walkSound.Stop();
        }

        walkSoundCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
