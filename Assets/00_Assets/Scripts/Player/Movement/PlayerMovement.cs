using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Stats
{
    public float speed = 3f;
    public float jumpForce = 1.5f;
    #region Singleton
    public static PlayerMovement Instance;
    private void Awake()
    {
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
            ResetStat(ref speed, 3f);
            ResetStat(ref jumpForce, 1.5f);
        }
    }
}
