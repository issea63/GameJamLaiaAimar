using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Stats
{
    public float speed = 15f;
    public float jumpForce = 6f;
    #region Singleton
    public static PlayerMovement Instance;
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
    private void Start()
    {
        SetStat(ref speed, 0);
        SetStat(ref jumpForce, 0);
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
            ResetStat(ref speed, 8);
            ResetStat(ref jumpForce, 5f);
        }
    }
}
