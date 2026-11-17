using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;

    public LayerMask groundLayer;
    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }
private void OnTriggerEnter(Collider other)
{
    if ((groundLayer.value & (1 << other.gameObject.layer)) != 0)
        playerController.isGrounded = true;
}

private void OnTriggerExit(Collider other)
{
    if ((groundLayer.value & (1 << other.gameObject.layer)) != 0)
        playerController.isGrounded = false;
}
}
