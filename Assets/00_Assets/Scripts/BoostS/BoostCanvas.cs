using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoostCanvas : MonoBehaviour
{
    public static BoostCanvas Instance;

    [SerializeField] private TMP_Text[] statsTexts;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        statsTexts = GetComponentsInChildren<TMP_Text>(true);
    }
    private void Start()
    {
        foreach (var text in statsTexts) text.enabled = false;
    }
    public void TurnOnTexts()
    {
        foreach (var text in statsTexts)
        {
            text.enabled = true;
        }
    }

    public void UpdateText(int index, string textStat)
    {
        foreach (var text in statsTexts)
        {
            text.enabled = true;
        }
        if (statsTexts == null || index < 0 || index >= statsTexts.Length)
        {
            Debug.LogWarning($"No text found at index {index}.", this);
            return;
        }

        statsTexts[index].text = textStat;
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
        foreach (var text in statsTexts)
        {
            text.enabled = true;
        }
        Debug.Log($"Loaded scene: {scene.name} (mode: {mode})");
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            foreach(var text in statsTexts)
            {
                text.enabled = false;
            }
        }
    }
}
