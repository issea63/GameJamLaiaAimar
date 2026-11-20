using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement moveStats;
    private Vector2 movementInput;
    Rigidbody rb;
    public bool isGrounded = true;
    public AudioSource jumpSound;
    public AudioSource walkSound;
    Transform cameraTransform;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        if(PlayerMovement.Instance == null)
        {
            GameObject playerMovementObject = new GameObject("PlayerMovement");
            moveStats = playerMovementObject.AddComponent<PlayerMovement>();
        }
        else
            moveStats = PlayerMovement.Instance;
    }
    public void OnMove(InputAction.CallbackContext input)
    {
        movementInput = input.ReadValue<Vector2>();
        if(walkSound != null && movementInput.magnitude > 0)
        {
            walkSound.pitch = Random.Range(0.8f, 1.2f);
            walkSound.Play();
        }
        else walkSound.Stop();
    }
    void FixedUpdate()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * movementInput.y + right * movementInput.x;
        direction = Vector3.ClampMagnitude(direction, 1f);

        rb.linearVelocity = new Vector3(direction.x * moveStats.speed, rb.linearVelocity.y, direction.z * moveStats.speed);
        gameObject.transform.rotation = Quaternion.Euler(0f, cameraTransform.localRotation.eulerAngles.y, 0f);
    }
    public void OnJump(InputAction.CallbackContext input)
    {
        if (input.performed && isGrounded)
        {
            isGrounded = false;
            Debug.Log("Jump button pressed");
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * moveStats.jumpForce, ForceMode.Impulse);
                if (jumpSound != null)
                {
                    jumpSound.Play();
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EXIT"))
        {
            SceneManager.LoadScene("Win");
        }

        if (other.CompareTag("GroundPlataformas"))
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
