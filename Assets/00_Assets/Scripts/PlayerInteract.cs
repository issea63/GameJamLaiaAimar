using System;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : InteractStats
{
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] bool interacting;
    [SerializeField] Interactable interactable;
    private void Update()
    {
        bool isHitting = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactDistance, interactableLayer);
        //interactable.ActivateCanvas();
        interactable = hit.transform.GetComponent<Interactable>();
        if (isHitting)
        {
            interacting = true;
            interactable.ActivateCanvas();
        }
        else
        {
            interacting = false;
            interactable.DeactivateCanvas();
        }
    }
    public void Interact()
    {
        if (interacting)
        {
            interactable.GetComponent<BoostStats>().ApplyBoost();
        }
        else return;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactDistance);
    }
}