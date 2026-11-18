using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    Image healthBarFill;
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}