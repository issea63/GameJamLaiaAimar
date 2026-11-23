using Unity.VisualScripting;
using UnityEngine;

public class InteractStats : Stats
{
    public float interactDistance;
    private void Start()
    {
        SetStat(ref interactDistance, 0);
    }
}
