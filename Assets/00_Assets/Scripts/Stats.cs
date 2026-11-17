using UnityEngine;

public class Stats : MonoBehaviour
{
    public virtual void SetStat(float oldStat, float newStat)
    {
        oldStat += newStat;
    }
    public virtual void ResetStat(float stat, float originalStat)
    {
        stat = originalStat;
    }
    public virtual void ReadStat(float stat)
    {

    }
}
