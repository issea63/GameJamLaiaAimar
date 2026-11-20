using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DoorOpen : MonoBehaviour
{
    private Rigidbody rb;
    public int doorOpenAmount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OpenDoor()
    {
        if (rb == null)
        {
            Debug.LogError("DoorOpen needs a Rigidbody on the same GameObject.", this);
            return;
        }

        rb.DOLocalPath(new[] { Vector3.zero, new Vector3(0, 0, doorOpenAmount) },2f, PathType.Linear);
    }

    public void CloseDoor()
    {
        if (rb == null)
        {
            Debug.LogError("DoorOpen needs a Rigidbody on the same GameObject.", this);
            return;
        }

        rb.DOLocalPath(new[] { new Vector3(0, 0, doorOpenAmount), Vector3.zero }, 2f, PathType.Linear);
    }
}
