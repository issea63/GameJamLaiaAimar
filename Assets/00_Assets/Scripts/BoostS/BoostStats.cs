using UnityEngine;

public class BoostStats : Stats
{
    public float boostAmount = 5f;
    public void ApplyBoost(float currentAmount, float boostAmount)
    {
        SetStat(ref currentAmount, boostAmount);
    }
}
