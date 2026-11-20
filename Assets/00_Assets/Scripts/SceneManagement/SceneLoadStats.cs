using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadStats : Stats
{
    #region Singleton
    public static SceneLoadStats Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public float levelProbChance = .4f;

    private void Start()
    {
        SetStat(ref levelProbChance, 0);
    }
    public void ResetLevelProbChance(float chance)
    {
        ResetStat(ref levelProbChance, .4f);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene: {scene.name} (mode: {mode})");
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ResetLevelProbChance(.4f);
        }
    }
}
