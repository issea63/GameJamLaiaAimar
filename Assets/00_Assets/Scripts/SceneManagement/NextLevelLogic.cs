using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;

public class NextLevelLogic : MonoBehaviour
{
    [SerializeField] CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    [SerializeField] DoorOpen[] doorOpens;
    [SerializeField] AudioSource elevatorSound;
    [SerializeField] CinemachinePanTilt panTilt;
    [SerializeField] CinemachineCamera camera;
    [SerializeField] SceneLoadStats sceneLoadStats;
    [SerializeField] SceneLoadManager sceneLoadManager;
    [SerializeField] bool isCoroutineRunning;
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
        if (sceneLoadManager == null)
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
        StartCoroutine(OnStart(2f, 3f));
    }
    public void SceneLoad()
    {
        if (isCoroutineRunning) return;
        else StartCoroutine(LoadSceneWithDelay(2f, 3f));
    }
    private IEnumerator LoadSceneWithDelay(float delayCloseDoor, float delay)
    {
        isCoroutineRunning = true;
        foreach (DoorOpen doorOpen in doorOpens)
        {
            doorOpen.CloseDoor();
            if (elevatorSound != null)
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
        isCoroutineRunning = false;
    }
    private IEnumerator OnStart(float delay, float delayCamera)
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
