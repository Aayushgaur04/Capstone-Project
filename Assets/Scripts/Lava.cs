using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is tagged "Player"
        if (collision.CompareTag("Player"))
        {
            Debug.Log("collision");
            // Get the PlayerHealth component from the player
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Deal damage equal to the player's current health so that they die instantly.
                playerHealth.TakeDamage(playerHealth.health);
            }
        }
    }
}
