using UnityEngine;

public class BossLaser : MonoBehaviour
{
    public int damage = 50;
    public float lifeTime = 1f; // how long the laser stays active

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
