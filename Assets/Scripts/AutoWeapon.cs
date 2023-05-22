using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile to be fired
    public float fireRate = 0.5f; // Time delay between each projectile fire
    public float projectileSpeed = 10f; // Speed of the projectile

    private float fireTimer = 0f; // Timer to track the time between each fire
    private playerAttack attack;

    private void Update()
    {
        // Update the fire timer
        fireTimer += Time.deltaTime;

        // Check if it's time to fire
        if (fireTimer >= fireRate)
        {
            FireProjectile();
            fireTimer = 0f; // Reset the fire timer
        }
    }

    private void FireProjectile()
    {
        // Instantiate the projectile prefab at the current position and rotation of the player
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Apply velocity to the projectile
        rb.velocity = projectile.transform.right * projectileSpeed;
    }
}
