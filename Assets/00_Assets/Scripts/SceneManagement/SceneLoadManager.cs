using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SceneLoadStats
{
    private int levelRandom = 0;
    private int finalLevelProbability = 0;

    private int currentSceneIndex;
    #region Singleton
    public static SceneLoadManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

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
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(levelRandom);
    }
    public void LastLevelLogic()
    {
        finalLevelProb = SceneLoadStats.Instance.finalLevelProb;
        finalLevelProbability = Random.Range(0, 100);
        if (finalLevelProbability <= finalLevelProb * 100)
        {
            levelRandom = 3;
        }
        else levelRandom = Random.Range(1, 3);
    }
}
