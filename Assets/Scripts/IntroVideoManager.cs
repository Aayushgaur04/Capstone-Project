using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipVideo();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadMainMenu();
    }

    void SkipVideo()
    {
        videoPlayer.Stop();
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(1); // or use "MainMenu" if using scene names
    }
}
