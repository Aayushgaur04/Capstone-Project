using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static bool allowInput = true;
    public Transform firePoint;
    public GameObject bulletPrefab;
    // Update is called once per frame
    void Update()
    {
        if (!allowInput) return;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0,0,0));
    }
}
