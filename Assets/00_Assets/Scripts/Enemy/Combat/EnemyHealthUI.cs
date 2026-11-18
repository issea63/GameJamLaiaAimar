using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField]
    Image healthBarFill;
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        //healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}
