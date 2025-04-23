using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    public GameObject gameOverUI; // assign the Panel
    public float delayBeforeGameOver = 2f; // Delay in seconds

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Optional: avoid duplicates
        }
    }

    public void EnableGameOverPanel()
    {
        StartCoroutine(ShowGameOverDelayed());
    }

    private IEnumerator ShowGameOverDelayed()
    {
        yield return new WaitForSecondsRealtime(delayBeforeGameOver); // waits even if timeScale is 0

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Time.timeScale = 0f;
        Weapon.allowInput = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Weapon.allowInput = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameData();
        }

        SceneManager.LoadScene(2); // Hub scene
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        Weapon.allowInput = true;
        SceneManager.LoadScene(1);
    }
}
