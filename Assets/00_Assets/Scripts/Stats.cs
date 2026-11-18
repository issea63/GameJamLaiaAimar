using UnityEngine;

public class Stats : MonoBehaviour
{
    public virtual void SetStat(ref float stat, float amount)
    {
        stat += amount;
    }

    public virtual void ResetStat(ref float stat, float originalStat)
    {
        stat = originalStat;
    }
}
