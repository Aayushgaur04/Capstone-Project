using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int collectibles = 0;

    private HashSet<string> completedPuzzles = new HashSet<string>();

    [HideInInspector] public string nextScene;
    [HideInInspector] public string nextSpawnID;

    [HideInInspector] public int playerHealth = 100;
    [HideInInspector] public int maxHealth = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFader();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompletePuzzle(string puzzleID)
    {
        if (!completedPuzzles.Contains(puzzleID))
        {
            completedPuzzles.Add(puzzleID);
            collectibles++;
        }
    }

    public bool HasCompletedPuzzle(string puzzleID)
        => completedPuzzles.Contains(puzzleID);

    void InitializeFader()
    {
        if (ScreenFader.Instance == null)
        {
            GameObject faderObj = Instantiate(Resources.Load<GameObject>("ScreenFader"));
            DontDestroyOnLoad(faderObj);
            StartCoroutine(FadeInOnStart());
        }
    }

    IEnumerator FadeInOnStart()
    {
        // Wait a frame for canvas to initialize
        yield return null;

        if (ScreenFader.Instance != null)
            yield return ScreenFader.Instance.FadeIn();
    }

    // Call this to go through a door
    public void GoTo(string sceneName, string spawnID = "")
    {
        StartCoroutine(LoadSceneWithFade(sceneName, spawnID));
    }

    private IEnumerator LoadSceneWithFade(string sceneName, string spawnID)
    {
        if (ScreenFader.Instance != null)
            yield return ScreenFader.Instance.FadeOut();

        nextScene = sceneName;
        nextSpawnID = spawnID;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        while (!op.isDone) yield return null;

        if (ScreenFader.Instance != null)
            yield return ScreenFader.Instance.FadeIn();
    }

    public void ResetGameData()
    {
        playerHealth = 100; // or whatever your max health is
        collectibles = 0;  // or however you're tracking collectibles
    }
}
