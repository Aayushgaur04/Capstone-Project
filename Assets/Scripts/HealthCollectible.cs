using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    // Amount of health to increase (each heart represents 25 health)
    public int healthIncreaseAmount = 25;

    // Optional: a collection effect (e.g., particle effect) to play when collected
    public GameObject collectEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is tagged "Player"
        if (other.CompareTag("Player"))
        {
            // Try to get the PlayerHealth component from the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Increase the player's health by the specified amount
                playerHealth.IncreaseMaxHealth(healthIncreaseAmount);

                // If a collect effect is assigned, instantiate it at the collectible's position
                if (collectEffect != null)
                {
                    Instantiate(collectEffect, transform.position, Quaternion.identity);
                }

                // Destroy this collectible after collection
                Destroy(gameObject);
            }
        }
    }
}
