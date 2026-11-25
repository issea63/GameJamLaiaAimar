using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : InteractStats
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Interactable interactable;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Interactable nextInteractable = null;

        if (mainCamera != null && Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, interactDistance, interactableLayer))
        {
            nextInteractable = hit.transform.GetComponent<Interactable>();
        }

        if (nextInteractable == interactable)
            return;

        if (interactable != null)
            interactable.DeactivateCanvas();

        interactable = nextInteractable;

        if (interactable != null)
            interactable.ActivateCanvas();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactable == null)
            return;
        if (context.performed) return;
        if (interactable.TryGetComponent<BoostStats>(out BoostStats boostStats))
            boostStats.ApplyBoost();
    }

    private void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        if (cam == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * interactDistance);
    }
}
