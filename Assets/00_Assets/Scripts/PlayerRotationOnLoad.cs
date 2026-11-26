using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRotationOnLoad : MonoBehaviour
{
    public static PlayerRotationOnLoad Instance { get; private set; }

    [SerializeField] private float pan;
    [SerializeField] private float tilt;

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

    public void CopyRotation(float panValue, float tiltValue)
    {
        pan = panValue;
        tilt = tiltValue;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CinemachineCamera camera = FindAnyObjectByType<CinemachineCamera>();

        if (camera == null)
        {
            Debug.LogWarning($"No se encontró una CinemachineCamera en {scene.name}.");
            return;
        }

        CinemachinePanTilt panTilt = camera.GetComponent<CinemachinePanTilt>();

        if (panTilt == null)
        {
            Debug.LogWarning("La CinemachineCamera no tiene el componente CinemachinePanTilt.");
            return;
        }

        panTilt.PanAxis.Value = pan;
        panTilt.TiltAxis.Value = tilt;
    }
}
