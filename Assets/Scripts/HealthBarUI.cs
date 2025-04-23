using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Image fillImage;

    void OnEnable()
    {
        Health.OnHealthChanged += UpdateHealthBar;
    }

    void OnDisable()
    {
        Health.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int current, int max)
    {
        fillImage.fillAmount = (float)current / max;
    }
}
