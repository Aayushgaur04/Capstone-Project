using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // Reference to the Game Over panel (assign in the Inspector)
    public GameObject gameOverPanel;

    private void Awake()
    {
        // Ensure the panel is disabled at the start.
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameOverPanel is not assigned in GameOverManager.");
        }
    }

    /// <summary>
    /// Enables the Game Over panel.
    /// </summary>
    public void EnableGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverPanel is not assigned in GameOverManager.");
        }
    }
}
