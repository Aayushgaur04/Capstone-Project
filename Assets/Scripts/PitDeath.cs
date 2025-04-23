using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PitDeath : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                // Instantly kill
                health.TakeDamage(health.maxHealth);
            }
        }
    }
}
