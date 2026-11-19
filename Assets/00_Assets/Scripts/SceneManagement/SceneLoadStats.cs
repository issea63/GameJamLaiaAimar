using UnityEngine;

public class SceneLoadStats : Stats
{
    public float levelProbChance = .4f;

    private void Start()
    {
        SetStat(ref levelProbChance, 0);
    }
    public void ResetLevelProbChance(float chance)
    {
        ResetStat(ref levelProbChance, .4f);
    }
}
