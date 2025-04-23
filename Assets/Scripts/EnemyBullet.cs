using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 25;
    public Rigidbody2D rb;
    public GameObject explosionEffectPrefab;
    public float lifeTime = 5f;

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision.collider);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HandleHit(collider);
    }

    void HandleHit(Collider2D hit)
    {
        Health playerHealth = hit.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
