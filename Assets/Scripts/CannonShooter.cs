using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;
    public float fireballSpeed = 10f;

    void Start()
    {
        InvokeRepeating(nameof(ShootFireball), 0f, fireInterval);
    }

    void ShootFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * fireballSpeed;
        }
    }
}
