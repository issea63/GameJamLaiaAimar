using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField]
    private int levelRandom = 0;
    [SerializeField]
    private int finalLevelProbability = 0;

    public float levelProbChance = 0.25f;
    private int currentSceneIndex;

    public void LoadRandomLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelRandom = Random.Range(1, 4);
        if (levelRandom == 3)
        {
            finalLevelProbability = Random.Range(0, 100);
            if (finalLevelProbability < levelProbChance * 100)
            {
                levelRandom = 3;
            }
            else levelRandom = Random.Range(1, 3);
        }
        while (levelRandom == currentSceneIndex)
        {
            levelRandom = Random.Range(1, 3);
        }
        SceneManager.LoadScene(levelRandom);
    }
}
