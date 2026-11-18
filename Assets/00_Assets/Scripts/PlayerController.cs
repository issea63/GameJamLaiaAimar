using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MovementPlayer moveStats;
    private Vector2 movementInput;

    public bool isGrounded = true;
    private void Start()
    {
        moveStats = MovementPlayer.Instance;
    }
    public void OnMove(InputAction.CallbackContext input)
    {
        movementInput = input.ReadValue<Vector2>();
    }

    private void Update()
    {
        float moveX = movementInput.x * moveStats.speed * Time.deltaTime;
        float moveZ = movementInput.y * moveStats.speed * Time.deltaTime;

        transform.Translate(moveX, 0, moveZ);

        gameObject.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
    public void OnJump(InputAction.CallbackContext input)
    {
        if (input.performed && isGrounded)
        {
            Debug.Log("Jump button pressed");
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * moveStats.jumpForce, ForceMode.Impulse);
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
