using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionElevator : MonoBehaviour
{
    public static PlayerPositionElevator Instance;

    [SerializeField] private Vector3 position;
    private bool hasSavedPosition;

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
        hasSavedPosition = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (hasSavedPosition)
            StartCoroutine(PlacePlayerAfterLoad());
    }

    private IEnumerator PlacePlayerAfterLoad()
    {
        yield return null;

        PlayerController player = FindAnyObjectByType<PlayerController>();
        GameObject spawnEnter = GameObject.Find("SpawnEnter");

        if (player == null || spawnEnter == null)
        {
            Debug.LogWarning("No se encontró el jugador o SpawnEnter.");
            yield break;
        }

        player.transform.position = spawnEnter.transform.TransformPoint(position);
    }
}
