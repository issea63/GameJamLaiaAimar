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
        if(other.CompareTag("Ground"))
        {
            playerController.isGrounded = true;
        }
        else
        {
            playerController.isGrounded = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            playerController.isGrounded = false;
        }
    }
}
