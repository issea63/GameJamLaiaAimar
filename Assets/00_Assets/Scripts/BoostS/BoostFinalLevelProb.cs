using UnityEngine;
using UnityEngine.SceneManagement;

public class BoostFinalLevelProb : BoostStats
{
    public SceneLoadStats sceneLoadStats;
    private void Start()
    {
        sceneLoadStats = SceneLoadStats.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            boostAmount = .2f;
            SetStat(ref sceneLoadStats.levelProbChance, boostAmount);
            BoostCanvas.Instance.UpdateText(3, "Final Level Chance: " + sceneLoadStats.levelProbChance);
            gameObject.SetActive(false);
        }
    }
}
