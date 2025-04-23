using UnityEngine;

public class FallingSword : MonoBehaviour
{
    public float fallSpeed = 10f;
    public int damage = 30;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
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

        if (!collision.CompareTag("Platform")) // Prevent hitting the boss itself
        {
            Destroy(gameObject);
        }
    }
}
