using UnityEngine;

public class EnemyCombatStats : Stats
{
    public float damage = 10f;
    public float cooldown = 1f;
    public float attackRange = 1f;

    private void Start()
    {
        SetStat(ref damage, 0);
        SetStat(ref cooldown, 0);
        SetStat(ref attackRange, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
