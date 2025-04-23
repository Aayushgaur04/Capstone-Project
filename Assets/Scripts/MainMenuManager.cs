using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Build Settings: 0 = MainMenu, 1 = Hub (Level 0)
    public void StartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGameData();
        }
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
