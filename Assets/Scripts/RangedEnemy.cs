using UnityEngine;

public class RangedPatrolEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 7f;
    public Transform leftEdge, rightEdge;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public int health = 100;
    public int damageToPlayer = 25;
    public GameObject deathEffect;

    private bool movingRight = true;
    private Transform player;
    private float nextFireTime;

    private bool playerInSight = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null && Mathf.Abs(player.position.y - transform.position.y) < 1.5f &&
            Mathf.Abs(player.position.x - transform.position.x) <= detectionRange)
        {
            playerInSight = true;

            // Determine if enemy needs to flip before firing
            float playerDirection = player.position.x - transform.position.x;
            if ((playerDirection > 0 && !movingRight) || (playerDirection < 0 && movingRight))
            {
                movingRight = !movingRight;
                Rotate();
            }

            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            playerInSight = false;
        }

        if (!playerInSight)
        {
            Patrol();
        }
    }


    void Patrol()
    {
        Vector3 target = movingRight ? rightEdge.position : leftEdge.position;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            movingRight = !movingRight;
            Rotate();
        }
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, movingRight ? 0 : 180, 0);
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        float direction = movingRight ? 1f : -1f;
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
                TakeDamage(bullet.damage);
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
        if (leftEdge != null && rightEdge != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(leftEdge.position, rightEdge.position);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
