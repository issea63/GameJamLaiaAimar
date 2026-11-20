using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ColliderScene : SceneLoadManager
{
    public CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public DoorOpen[] doorOpens;
    public AudioSource elevatorSound;
    [SerializeField]
    public SceneLoadStats sceneLoadStats;
    private void Start()
    {
        if (sceneLoadStats == null)
        {
            GameObject playerMovementObject = new GameObject("SceneLoadStats");
            sceneLoadStats = playerMovementObject.AddComponent<SceneLoadStats>();
        }
        else
        {
            sceneLoadStats = SceneLoadStats.Instance;
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
        LoadRandomLevel();
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
