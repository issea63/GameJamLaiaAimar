using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public int levelRandom = 0;
    public int finalLevelProbability = 0;
    public float levelProbChance = 0.1f;

    public void LoadRandomLevel()
    {
        levelRandom = Random.Range(1, 4);
        if(levelRandom == 4)
        {
            finalLevelProbability = Random.Range(0, 100);
            if (finalLevelProbability < levelProbChance * 100)
            {
                levelRandom = Random.Range(1, 3);
            }
            else levelRandom = 4;
        }
        SceneManager.LoadScene(levelRandom);
    }
}
