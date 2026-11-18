using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;

    public LayerMask groundLayer;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == groundLayer)
            playerController.isGrounded = true;
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == groundLayer)
            playerController.isGrounded = false;
    }*/

}
