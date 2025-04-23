using UnityEngine;

public class PersistentPlayerUI : MonoBehaviour
{
    public static PersistentPlayerUI Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
