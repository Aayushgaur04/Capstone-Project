using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public Health playerHealth;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))  // Press H to take damage
        {
            playerHealth.TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.J))  // Press J to heal to full
        {
            playerHealth.HealToMax();
        }
    }
}