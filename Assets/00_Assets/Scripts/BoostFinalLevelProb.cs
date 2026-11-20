using UnityEngine;

public class BoostFinalLevelProb : BoostStats
{
    SceneLoadStats sceneLoadStats;
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ApplyBoost(sceneLoadStats.levelProbChance, boostAmount);
            gameObject.SetActive(false);
        }
    }
}
