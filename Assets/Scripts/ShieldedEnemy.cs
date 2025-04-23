using UnityEngine;

public class ShieldedShooterEnemy : MonoBehaviour
{
    public float detectionRange = 7f;
    public Transform firePoint;
    public GameObject fireballPrefab;
    public float fireRate = 1f;
    public int health = 100;
    public int damageToPlayer = 25;
    public GameObject deathEffect;

    private Transform player;
    private float nextFireTime;
    private bool playerInFront = false;
    private bool facingRight = true; // Set based on your default facing direction

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Detect facing direction based on localScale.x
        facingRight = Mathf.RoundToInt(transform.eulerAngles.y) != 180;
    }

    void Update()
    {
        if (player != null && Mathf.Abs(player.position.y - transform.position.y) < 1.5f)
        {
            if (IsPlayerInFront() && Vector2.Distance(player.position, transform.position) <= detectionRange)
            {
                playerInFront = true;

                if (Time.time >= nextFireTime)
                {
                    Fire();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
            else
            {
                playerInFront = false;
            }
        }
        else
        {
            playerInFront = false;
        }
    }

    bool IsPlayerInFront()
    {
        if (player == null) return false;

        if (facingRight)
        {
            return player.position.x > transform.position.x;
        }
        else
        {
            return player.position.x < transform.position.x;
        }
    }

    void Fire()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        float direction = facingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * 10f, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health healthScript = collision.GetComponent<Health>();
            if (healthScript != null)
            {
                healthScript.TakeDamage(damageToPlayer);
            }

            Die();
        }

        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                // Check if the bullet hits from behind
                Vector2 toBullet = collision.transform.position - transform.position;
                bool hitFromBehind = (facingRight && toBullet.x < 0) || (!facingRight && toBullet.x > 0);

                if (hitFromBehind)
                {
                    TakeDamage(bullet.damage);
                }
                else
                {
                    // Optional: block/deflect bullet
                    Destroy(collision.gameObject);
                }
            }
        }
    }

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
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw facing direction line
        Gizmos.color = Color.yellow;
        Vector3 direction = facingRight ? Vector3.right : Vector3.left;
        Gizmos.DrawLine(transform.position, transform.position + direction * detectionRange);
    }
}
