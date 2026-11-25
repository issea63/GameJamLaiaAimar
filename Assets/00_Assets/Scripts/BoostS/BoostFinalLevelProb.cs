using UnityEngine;
using UnityEngine.SceneManagement;

public class BoostFinalLevelProb : BoostStats
{
    public SceneLoadStats sceneLoadStats;
    private void Start()
    {
        boostAmount = .2f;
        sceneLoadStats = SceneLoadStats.Instance;
        textSet = ("Final Level Prob. + " + boostAmount*100 + "%");
    }
    public override void ApplyBoost()
    {
        base.ApplyBoost();
        boostAmount = .2f;
        SetStat(ref sceneLoadStats.levelProbChance, boostAmount);
        boostAmount = 0;
        BoostCanvas.Instance.UpdateText(3, "Final Level Chance: " + sceneLoadStats.levelProbChance * 100 + "%");
        gameObject.SetActive(false);
    }
}
