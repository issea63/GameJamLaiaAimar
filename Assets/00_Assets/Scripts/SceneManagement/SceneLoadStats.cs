using Unity.VisualScripting;
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
    public float finalLevelProb = .4f;
    [SerializeField] float originalLevelProb = .4f;

    private void Start()
    {
        ResetLevelProbChance(originalLevelProb);
    }
    public void ResetLevelProbChance(float chance)
    {
        ResetStat(ref finalLevelProb, chance);
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
        BoostCanvas.Instance.UpdateText(3, "Final Level Chance: " + finalLevelProb * 100 + "%");
        Debug.Log($"Loaded scene: {scene.name} (mode: {mode})");
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ResetLevelProbChance(originalLevelProb);
            BoostCanvas.Instance.UpdateText(3, "Final Level Chance: " + finalLevelProb * 100 + "%");
        }
        Cursor.lockState = CursorLockMode.None;
    }
}
