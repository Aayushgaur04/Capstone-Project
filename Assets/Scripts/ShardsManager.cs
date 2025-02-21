using System.Collections;
using UnityEngine;
using TMPro;

public class ShardsManager : MonoBehaviour
{

    public TextMeshProUGUI coinCounterText; // Reference to the TextMeshPro UI element
    public GameObject gameOverPanel; // Reference to the Game Over UI panel
    public int coinCount = 0; // Coin counter

    void Start()
    {
        UpdateCoinCounter(); // Initialize the UI
        gameOverPanel.SetActive(false); // Ensure Game Over panel is hidden initially
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shard"))
        {
            coinCount++; // Increment the coin count
            UpdateCoinCounter(); // Update the UI
            Destroy(other.gameObject); // Remove the coin from the scene

            if (coinCount >= 3)
            {
                StartCoroutine(GameOverWithDelay());
            }
        }
    }

    void UpdateCoinCounter()
    {
        coinCounterText.text = "" + coinCount; // Update the visible counter
    }

    IEnumerator GameOverWithDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        GameOver(); // Show the Game Over screen
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true); // Show Game Over UI
        Time.timeScale = 0f; // Pause the game (optional)
    }
}
