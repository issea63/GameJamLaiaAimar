using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRotationOnLoad : MonoBehaviour
{
    #region Singleton

    public static PlayerRotationOnLoad Instance;
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

    [SerializeField] Quaternion rotation;

    Camera cam;
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
        CinemachineCamera camera = FindAnyObjectByType<CinemachineCamera>();
        CinemachinePanTilt panTilt = camera.GetComponent<CinemachinePanTilt>();
        panTilt.PanAxis.Value = rotation.y;
        panTilt.TiltAxis.Value = rotation.x;
    }
    public void CopyRotation(float vectorX, float vectorY)
    {
        rotation.x = vectorX;
        rotation.y = vectorY;
    }
}
