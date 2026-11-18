using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Rigidbody rb;
    public int doorOpenAmount;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void CloseDoor()
    {
        DOTweenModulePhysics.DOLocalPath(rb, new Vector3[] { new Vector3(0, 0, doorOpenAmount), new Vector3(0, 0, 0) }, 2f, PathType.Linear);
    }
    public void OpenDoor()
    {
        DOTweenModulePhysics.DOLocalPath(rb, new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, doorOpenAmount) }, 2f, PathType.Linear);
    }
}

