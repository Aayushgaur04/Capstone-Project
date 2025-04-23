using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameCompletionManager : MonoBehaviour
{
    public GameObject completionPanel;

    public void ShowCompletionScreen()
    {
        StartCoroutine(DelayedShow());
    }

    private IEnumerator DelayedShow()
    {
        yield return new WaitForSeconds(3f);
        completionPanel.SetActive(true);
        Time.timeScale = 0f;
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
