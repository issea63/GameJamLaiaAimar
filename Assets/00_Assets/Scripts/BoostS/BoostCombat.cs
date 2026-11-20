using UnityEngine;

public class BoostCombat : BoostStats
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerCombatStats playerCombat = PlayerCombatStats.Instance;

        if (playerCombat == null)
        {
            Debug.LogError("PlayerCombatStats.Instance es null.");
            return;
        }

        playerCombat.SetStat(ref playerCombat.damage, boostAmount);
        BoostCanvas.Instance.UpdateText(0, "Damage: " + playerCombat.damage);
        gameObject.SetActive(false);
    }
}
