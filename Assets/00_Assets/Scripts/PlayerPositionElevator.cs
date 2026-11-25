using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerPositionElevator : MonoBehaviour
{
    #region Singleton

    public static PlayerPositionElevator Instance;
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

    [SerializeField] Vector3 position;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void CopyTransform()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        GameObject spawnExit = GameObject.Find("SpawnExit");

        if (player == null || spawnExit == null)
            return;

        position = spawnExit.transform.InverseTransformPoint(player.transform.position);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        GameObject spawnEnter = GameObject.Find("SpawnEnter");

        if (player == null || spawnEnter == null)
            return;

        player.transform.position = spawnEnter.transform.TransformPoint(position);
    }
}
