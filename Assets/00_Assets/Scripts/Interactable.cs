using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    BoostStats stats;
    public GameObject canvas;
    public TextMeshProUGUI text;
    private void Start()
    {
        gameObject.layer = 9;
    }

    public void ActivateCanvas()
    {
        canvas.SetActive(true);
        if(canvas.TryGetComponent(out BillboardCanvas billboard))
        {
            Debug.Log("Ya tiene el BillBoard Canvas");
        }
        else canvas.AddComponent<BillboardCanvas>();

        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
        if(stats == null) stats = GetComponent<BoostStats>();

        text.text = stats.textSet;
    }
    public void DeactivateCanvas()
    {
        canvas.SetActive(false);
    }
}
