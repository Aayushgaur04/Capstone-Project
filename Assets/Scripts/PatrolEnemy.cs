using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform pointA;         // Left patrol point
    public Transform pointB;         // Right patrol point
    public float patrolSpeed = 2f;   // Patrol speed

    [Header("Shooting Settings")]
    public Transform firePoint;      // Bullet spawn position (child of enemy)
    public GameObject bulletPrefab;  // Bullet prefab (shared with player)
    public float fireRate = 1f;      // Seconds between shots

    [Header("Player Reference")]
    public Transform player;         // Reference to the player

    [Header("Animation")]
    public Animator animator;        // Reference to the enemy's Animator

    private float fireTimer = 0f;
    private bool movingToB = true;
    // Track the enemy's facing direction (true means facing right)
    private bool facingRight = true;

    private void Update()
    {
        // Determine patrol boundaries from pointA and pointB.
        float leftBound = Mathf.Min(pointA.position.x, pointB.position.x);
        float rightBound = Mathf.Max(pointA.position.x, pointB.position.x);

        // If the player's x-position is within the patrol range, attack (stop and shoot).
        if (player.position.x >= leftBound && player.position.x <= rightBound)
        {
            AttackMode();
        }
        else
        {
            PatrolMode();
        }
    }

    /// <summary>
    /// In attack mode, the enemy stops moving, faces the player, and shoots.
    /// </summary>
    private void AttackMode()
    {
        // Set animation parameters: stop moving and start shooting.
        if (animator != null)
        {
            animator.SetFloat("speed", 0f);
            animator.SetBool("isShooting", true);
        }

        // Face the player using our Flip method.
        if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }

        // Remain stationary and shoot at intervals.
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    /// <summary>
    /// In patrol mode, the enemy moves between pointA and pointB.
    /// </summary>
    private void PatrolMode()
    {
        // Set animation parameters: moving and not shooting.
        if (animator != null)
        {
            animator.SetBool("isShooting", false);
            // Here we simply set speed to 1 to indicate movement.
            animator.SetFloat("speed", 1f);
        }

        Transform targetPoint = movingToB ? pointB : pointA;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

        // Face the direction of travel.
        if (targetPoint.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (targetPoint.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }

        // Switch direction if close enough to the target.
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            movingToB = !movingToB;
        }
    }

    /// <summary>
    /// Flips the enemy (and its children) by rotating 180° around the Y-axis.
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    /// <summary>
    /// Shoots a bullet. The bullet is instantiated with a rotation so that its transform.up points in the intended direction.
    /// </summary>
    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Bullet prefab or firePoint is not assigned!");
            return;
        }

        // Use the same method as the player's weapon script:
        // The additional rotation offset (-90°) aligns the bullet's up vector with the intended shot direction.
        Quaternion bulletRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, 0f);
        Instantiate(bulletPrefab, firePoint.position, bulletRotation);
    }
}
