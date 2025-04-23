using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        GameManager.Instance.collectibles++;
        // Optional: play sound or UI update
        Destroy(gameObject);
    }
}