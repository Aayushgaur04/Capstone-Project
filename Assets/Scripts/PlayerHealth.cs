using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
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
