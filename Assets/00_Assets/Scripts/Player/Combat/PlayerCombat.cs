using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BoxCollider attackCollider;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private float cooldownDuration = 1f;
    [SerializeField] private float attackDuration = 0.5f;

    private PlayerCombatStats combatStats;
    private bool canAttack = true;

    private void Awake()
    {
        combatStats = PlayerCombatStats.Instance;

        if (combatStats == null)
            Debug.LogError("No se encontró PlayerCombatStats.Instance.", this);

        if (attackCollider == null)
            Debug.LogError("Falta asignar attackCollider en el Inspector.", this);

        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    public void Attack()
    {
        if (!canAttack || attackCollider == null)
            return;

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        canAttack = false;
        attackCollider.enabled = true;

        if (attackSound != null)
            attackSound.Play();

        yield return new WaitForSeconds(attackDuration);

        attackCollider.enabled = false;

        yield return new WaitForSeconds(cooldownDuration);

        canAttack = true;
    }
}
