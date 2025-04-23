using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUIManager : MonoBehaviour
{
    public static BossUIManager Instance;

    public Image healthBarFill;
    public TextMeshProUGUI bossNameText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBossName(string name)
    {
        bossNameText.text = name;
    }

    public void SetHealth(float current, float max)
    {
        healthBarFill.fillAmount = current / max;
    }

    public void ShowUI(bool show)
    {
        gameObject.SetActive(show);
    }
}
