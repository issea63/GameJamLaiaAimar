using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    Image healthBarFill;
    private void Start()
    {
        if (healthBarFill == null)
        {
            healthBarFill = GameObject.Find("HealthBar").GetComponent<Image>();
        }
        else
        {
            return;
        }
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            return;
        }
    }
}