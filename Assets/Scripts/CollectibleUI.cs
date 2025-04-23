using UnityEngine;
using TMPro;

public class CollectibleUI : MonoBehaviour
{
    public TextMeshProUGUI collectibleText;

    void Update()
    {
        if (collectibleText == null || GameManager.Instance == null) return;

        collectibleText.text = GameManager.Instance.collectibles.ToString();
    }
}