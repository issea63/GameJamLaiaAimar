using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject canvas;
    private void Start()
    {
        gameObject.layer = 9;
    }

    public void ActivateCanvas()
    {
        canvas.SetActive(true);
        if(canvas.TryGetComponent(out Interactable interactable))
        {
            Debug.Log("Ya tiene el BillBoard Canvas");
        }
        else canvas.AddComponent<BillboardCanvas>();
    }
    public void DeactivateCanvas()
    {
        canvas.SetActive(false);
    }
}
