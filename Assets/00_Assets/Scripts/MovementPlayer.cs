using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementPlayer : Stats
{
    public float speed = 3f;
    public float jumpForce = 1.5f;
    #region Singleton
    public static MovementPlayer Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    private void Start()
    {
        SetStat(speed, 0);
        SetStat(jumpForce, 0);
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
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            ResetStat(speed, 3f);
            ResetStat(jumpForce, 1.5f);
        }
    }
}
