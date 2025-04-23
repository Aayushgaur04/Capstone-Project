using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioClip mainMenuMusic;
    public AudioClip normalLevelMusic;
    public AudioClip bossMusic;
    public float fadeDuration = 1.5f; // How long fading takes

    private AudioSource audioSource;
    private string currentSceneName = "";
    private Coroutine fadeCoroutine;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == currentSceneName)
            return;

        currentSceneName = scene.name;

        AudioClip clipToPlay = null;

        if (scene.name == "Main Menu")
        {
            clipToPlay = mainMenuMusic;
        }
        else if (scene.name == "Final Level")
        {
            clipToPlay = bossMusic;
        }
        else if (scene.name.StartsWith("Level"))
        {
            clipToPlay = normalLevelMusic;
        }
        else
        {
            Debug.LogWarning("MusicManager: Scene name not recognized for music assignment: " + scene.name);
        }

        if (clipToPlay != null)
        {
            if (audioSource.clip != clipToPlay)
            {
                if (fadeCoroutine != null)
                    StopCoroutine(fadeCoroutine);

                fadeCoroutine = StartCoroutine(FadeToNewTrack(clipToPlay));
            }
        }
    }

    IEnumerator FadeToNewTrack(AudioClip newClip)
    {
        // Fade out
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = startVolume; // Make sure it's exactly back
    }
}
