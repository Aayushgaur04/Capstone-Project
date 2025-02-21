using UnityEngine;

public class PlayerHeartUI : MonoBehaviour
{
    // Reference to the player's health script
    public PlayerHealth playerHealth;

    // The UI panel (or container) where hearts will appear.
    public Transform heartsPanel;

    // Prefab for a single heart image (with your heart sprite)
    public GameObject heartPrefab;

    // Keep track of how many hearts were previously shown,
    // so we only update the UI when necessary.
    private int previousHeartCount = -1;

    void Update()
    {
        // Calculate the number of hearts based on current health.
        // For example, if health is 100, hearts = 4; if health is 75, hearts = 3.
        int heartCount = playerHealth.health / 25;

        if (heartCount != previousHeartCount)
        {
            UpdateHearts(heartCount);
            previousHeartCount = heartCount;
        }
    }

    void UpdateHearts(int heartCount)
    {
        // Remove all current heart icons.
        foreach (Transform child in heartsPanel)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a new heart icon for each heart the player should have.
        for (int i = 0; i < heartCount; i++)
        {
            Instantiate(heartPrefab, heartsPanel);
        }
    }
}
