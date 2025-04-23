using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [Header("Scene & Spawn")]
    [Tooltip("Name of the scene this door loads")]
    public string sceneToLoad;

    [Tooltip("Spawn point ID in the target scene")]
    public string spawnID = "";

    [Header("Lock Settings")]
    [Tooltip("If non empty, this puzzle must be done to pass")]
    public string requiredPuzzleID = "";
    [Tooltip("If true, player must have at least requiredCollectibles")]
    public bool lockedUntilCollected = false;
    [Tooltip("How many collectibles required to unlock")]
    public int requiredCollectibles = 4;

    [Header("Lock Icon")]
    [Tooltip("Floating lock icon to show when locked")]
    public GameObject lockIconPrefab;
    [Tooltip("Vertical offset for the lock icon")]
    public Vector3 lockIconOffset = new Vector3(0, 2f, 0);

    private GameObject lockInstance;

    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!string.IsNullOrEmpty(requiredPuzzleID))
        {
            if (!GameManager.Instance.HasCompletedPuzzle(requiredPuzzleID))
            {
                ShowLockIcon();
                return;
            }
        }
        else if (lockedUntilCollected &&
                 GameManager.Instance.collectibles < requiredCollectibles)
        {
            ShowLockIcon();
            return;
        }

        GameManager.Instance.GoTo(sceneToLoad, spawnID);
    }

    private void ShowLockIcon()
    {
        if (lockInstance != null) return;

        lockInstance = Instantiate(
            lockIconPrefab,
            transform.position + lockIconOffset,
            Quaternion.identity
        );

        Destroy(lockInstance, 1f);
    }
}
