using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SceneLoadStats
{
    private int levelRandom = 0;
    private int finalLevelProbability = 0;

    private int currentSceneIndex;
    private void Start()
    {
        ResetLevelProbChance(.4f);
    }
    public void LoadRandomLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelRandom = Random.Range(1, 4);
        if (levelRandom == 3)
        {
            LastLevelLogic();
        }
        while (levelRandom == currentSceneIndex)
        {
            levelRandom = Random.Range(1, 4);
            LastLevelLogic();
        }
        SceneManager.LoadScene(levelRandom);
    }
    public void LastLevelLogic()
    {
        finalLevelProbability = Random.Range(0, 100);
        if (finalLevelProbability <= levelProbChance * 100)
        {
            levelRandom = 3;
        }
        else levelRandom = Random.Range(1, 3);
    }
}
