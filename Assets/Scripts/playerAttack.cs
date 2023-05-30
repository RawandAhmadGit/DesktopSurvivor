using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private playerScript player; // Reference to the playerScript
    public float attackStrength;
    public float knockbackStrength;
    public float projectileDuration = 3f;
    private float currentDuration = 0f;
    public List<GenericEnemy> hitEnemies;

    private void Start()
    {
        // Get the playerScript component from the player object
    }

    private void Update()
    {
        // Increase the current duration of the projectile
        currentDuration += Time.deltaTime;

        // Check if the projectile exceeds the maximum duration
        if (currentDuration >= projectileDuration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // Check if the collided object is an enemy
        GenericEnemy enemy = collision.gameObject.GetComponent<GenericEnemy>();
        if (enemy != null)
        {
            // Handle the enemy hit with attack and knockback
            RegisterHitEnemy(enemy);

            // Destroy the projectile
            Destroy(gameObject);
        }
    }

    public void RegisterHitEnemy(GenericEnemy enemy)
    {
        // Store the hit enemy and perform any necessary actions
        hitEnemies.Add(enemy);

        // Example: Apply additional effects or logic when an enemy is hit
    }
}
