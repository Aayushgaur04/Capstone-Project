using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // assign the Panel

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("ResumeGame button clicked");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Weapon.allowInput = true;
        isPaused = false;
    }

    void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Weapon.allowInput = false;
        isPaused = true;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        Weapon.allowInput = true;
        SceneManager.LoadScene(1); // MainMenu scene index
    }
}
