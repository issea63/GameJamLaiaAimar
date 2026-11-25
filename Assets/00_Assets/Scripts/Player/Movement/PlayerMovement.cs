using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Stats
{
    public float speed = 8f;
    public float jumpForce = 4f;
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
        BoostCanvas.Instance.UpdateText(1, "Speed: " + speed);
        BoostCanvas.Instance.UpdateText(2, "Jump Force: " + jumpForce);
        Debug.Log($"Loaded scene: {scene.name} (mode: {mode})");
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            ResetStat(ref speed, 8);
            ResetStat(ref jumpForce, 4f);
            BoostCanvas.Instance.UpdateText(1, "Speed: " + speed);
            BoostCanvas.Instance.UpdateText(2, "Jump Force: " + jumpForce);
        }
    }
}
