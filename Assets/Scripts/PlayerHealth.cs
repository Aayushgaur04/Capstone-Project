using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;       // current health
    public int maxHealth = 100;    // maximum health
    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;

        if (health <= 0)
        {
            Die();
        }
    }

    // This method can be called when a heart collectible is picked up.
    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        health += amount; // Optionally also restore health or not.
    }

    void Die()
    {
        // Instantiate death effect
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Enable the Game Over panel before destroying the player.
        GameOverManager manager = FindObjectOfType<GameOverManager>();
        if (manager != null)
        {
            manager.EnableGameOverPanel();
        }
        else
        {
            Debug.LogWarning("GameOverManager not found in the scene.");
        }

        // Destroy the player GameObject.
        Destroy(gameObject);
    }
}
