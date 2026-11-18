using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    public PlayerCombatStats playerCombatStats;

    private void Start()
    {
        playerCombatStats = GetComponentInParent<PlayerCombatStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(playerCombatStats.damage);
            }
        }
    }
}
