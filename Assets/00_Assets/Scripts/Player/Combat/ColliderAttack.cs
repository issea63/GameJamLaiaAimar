using UnityEngine;

public class ColliderAttack : MonoBehaviour
{
    public PlayerCombatStats playerCombatStats;

    private void Start()
    {
        playerCombatStats = PlayerCombatStats.Instance;
        if(playerCombatStats == null)
        {
            GameObject playerMovementObject = new GameObject("PlayerCombatStats");
            playerCombatStats = playerMovementObject.AddComponent<PlayerCombatStats>();
        }
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
