using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerCombatStats combatStats;
    public BoxCollider attackCollider;
    public AudioSource attackSound;
    private void Start()
    {
        combatStats = GetComponent<PlayerCombatStats>();
    }
    public void Attack()
    {
        StartCoroutine(attackRoutine());
    }
    public IEnumerator attackRoutine()
    {
        if (combatStats.cooldown <= 0)
        {
            attackCollider.enabled = true;
            combatStats.cooldown = 1f;
            yield return new WaitForSeconds(0.5f);
            attackCollider.enabled = false;
            if (attackSound != null)
            {
                attackSound.Play();
            }
            else attackCollider.enabled = false; yield break;
        }
    }
}
