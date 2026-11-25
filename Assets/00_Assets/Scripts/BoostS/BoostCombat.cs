using UnityEngine;

public class BoostCombat : BoostStats
{
    private void Start()
    {
        boostAmount = 4;
        textSet = ("Damage + " + boostAmount);
    }
    public override void ApplyBoost()
    {
        base.ApplyBoost();
        PlayerCombatStats playerCombat = PlayerCombatStats.Instance;

        if (playerCombat == null)
        {
            Debug.LogError("PlayerCombatStats.Instance es null.");
            return;
        }
        boostAmount = 4;
        playerCombat.SetStat(ref playerCombat.damage, boostAmount);
        boostAmount = 0;
        BoostCanvas.Instance.UpdateText(0, "Damage: " + playerCombat.damage);
        gameObject.SetActive(false);
    }
}
