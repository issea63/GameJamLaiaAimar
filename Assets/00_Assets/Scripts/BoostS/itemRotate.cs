using DG.Tweening;
using UnityEngine;

public class itemRotate : MonoBehaviour
{
    Rigidbody rb;
    Vector3 rotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void LateUpdate()
    {
        rotation = new Vector3(0, 0, 360);
        rb.DORotate(rotation, 2, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Yoyo);
    }


}
