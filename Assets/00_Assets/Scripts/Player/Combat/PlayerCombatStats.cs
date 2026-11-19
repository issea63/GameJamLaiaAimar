using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombatStats : Stats
{
    public float damage = 15f;
    public float cooldown = 1f;
    #region Singleton
    public static PlayerCombatStats Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    private void Start()
    {
        SetStat(ref damage, 0);
        SetStat(ref cooldown, 0);
    }
    private void Update()
    {
        if(cooldown > 0)
        {
            SetStat(ref cooldown, -Time.deltaTime);
            if(cooldown < 0)
            {
                ResetStat(ref cooldown, 0);
            }
        }
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
            ResetStat(ref damage, 8);
            ResetStat(ref cooldown, 1f);
        }
    }
}
