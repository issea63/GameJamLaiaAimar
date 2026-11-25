using System.Collections;
using System.Timers;
using Unity.Cinemachine;
using UnityEngine;

public class ColliderScene : MonoBehaviour
{
    public CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public DoorOpen[] doorOpens;
    public AudioSource elevatorSound;
    [SerializeField]
    CinemachinePanTilt panTilt;
    [SerializeField]
    CinemachineCamera camera;
    [SerializeField]
    public SceneLoadStats sceneLoadStats;
    public SceneLoadManager sceneLoadManager;
    private void Start()
    {
        if (sceneLoadStats == null)
        {
            GameObject sceneLoadObject = new GameObject("SceneLoadStats");
            sceneLoadStats = sceneLoadObject.AddComponent<SceneLoadStats>();
            sceneLoadStats = SceneLoadStats.Instance;
        }
        else
        {
            sceneLoadStats = SceneLoadStats.Instance;
        }
        if(sceneLoadManager == null)
        {
            GameObject sceneloadManagerInstance = new GameObject("SceneLoadManager");
            sceneLoadManager = sceneloadManagerInstance.AddComponent<SceneLoadManager>();
            sceneLoadManager = SceneLoadManager.instance;
        }
        else
        {
            sceneLoadManager = SceneLoadManager.instance;
        }
        //ResetLevelProbChance(.4f);
        cinemachineBasicMultiChannelPerlin = FindAnyObjectByType<CinemachineBasicMultiChannelPerlin>();
        
        doorOpens = FindObjectsByType<DoorOpen>();
        StartCoroutine(OnStart(2f,3f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ascensor"))
        {
            StartCoroutine(LoadSceneWithDelay(2f, 3f));
        }
    }
    public IEnumerator LoadSceneWithDelay(float delayCloseDoor, float delay)
    {
        foreach (DoorOpen doorOpen in doorOpens)
        {
            doorOpen.CloseDoor();
            if(elevatorSound != null)
            {
                elevatorSound.Play();
            }
        }
        yield return new WaitForSeconds(delayCloseDoor);
        cinemachineBasicMultiChannelPerlin.enabled = true;
        yield return new WaitForSeconds(delay);
        PlayerRotationOnLoad playerRotationOn = PlayerRotationOnLoad.Instance;
        camera = FindAnyObjectByType<CinemachineCamera>();
        panTilt = camera.GetComponent<CinemachinePanTilt>();
        playerRotationOn.CopyRotation(panTilt.TiltAxis.Value, panTilt.PanAxis.Value);
        PlayerPositionElevator elevatorPos = PlayerPositionElevator.Instance;
        elevatorPos.CopyTransform();
        SceneLoadManager.instance.LoadRandomLevel();
    }
    public IEnumerator OnStart(float delay, float delayCamera)
    {
        cinemachineBasicMultiChannelPerlin.enabled = true;
        if (elevatorSound != null)
        {
            elevatorSound.Play();
        }
        yield return new WaitForSeconds(delay);
        cinemachineBasicMultiChannelPerlin.enabled = false;
        yield return new WaitForSeconds(delayCamera);
        foreach (DoorOpen doorOpen in doorOpens)
        {
            doorOpen.OpenDoor();
        }
    }
}
