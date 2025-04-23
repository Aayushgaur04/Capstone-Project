using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public static event Action<int, int> OnHealthChanged;

    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage Settings")]
    public float invincibilityDuration = 1f;
    private float lastDamageTime = -999f;

    [Header("Death")]
    public GameObject deathEffect;

    void Awake()
    {
        currentHealth = GameManager.Instance != null ? GameManager.Instance.playerHealth : maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (Time.time - lastDamageTime < invincibilityDuration) return;

        lastDamageTime = Time.time;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HealToMax()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void Die()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        GameOverManager.Instance?.EnableGameOverPanel();

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerHealth = currentHealth;
        }
    }
}
