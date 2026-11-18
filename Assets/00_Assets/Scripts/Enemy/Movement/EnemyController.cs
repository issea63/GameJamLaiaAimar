using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    EnemyMovement moveStats;
    public float detectionRange = 5f;
    public LayerMask playerLayer;
    public bool playerDetected = false;
    NavMeshAgent agent;
    public Transform[] patrolPoints;
    [SerializeField]
    int patrolIndex = 0;
    bool isPatrolling = true;
    public AudioSource walkSound;
    EnemyCombatStats enemyCombatStats;
    bool agentStopped = false;
    EnemyCombat combat;
    private void Start()
    {
        combat = GetComponent<EnemyCombat>();
        enemyCombatStats = GetComponent<EnemyCombatStats>();
        agent = GetComponent<NavMeshAgent>();
        if (moveStats == null)
        {
            GameObject movementEnemyObject = new GameObject("MovementEnemy");
            moveStats = movementEnemyObject.AddComponent<EnemyMovement>();
        }
    }
    private void Update()
    {
        DetectPlayer();
        if (agent.isStopped)
        {
            agentStopped = true;

        }
        else
        {
            agentStopped = false;
        }
    }
        
    public void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position,detectionRange,playerLayer);

        if (hits.Length > 0)
        {
            isPatrolling = false;
            playerDetected = true;
            agent.SetDestination(hits[0].transform.position);
            if(Vector3.Distance(transform.position, hits[0].transform.position) <= enemyCombatStats.attackRange)
            {
                agent.isStopped = true;
                if (enemyCombatStats != null)
                {
                    combat.AttackPlayer();
                }
            }
            if (walkSound != null && !agentStopped)
            {
                //walkSound.pitch = Random.Range(0.8f, 1.2f);
                walkSound.Play();
            }
        }
        else
        {
            playerDetected = false;
            Patrol();
        }
    }
    public void Patrol()
    {
        isPatrolling = true;
        if (patrolPoints.Length == 0) return;

        if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) < agent.stoppingDistance && isPatrolling)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        agent.SetDestination(patrolPoints[patrolIndex].position);
        if (walkSound != null && !agentStopped)
        {
            walkSound.pitch = Random.Range(0.8f, 1.2f);
            walkSound.Play();
        }
        else return;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}