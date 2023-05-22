using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    private playerScript player; // Reference to the playerScript
    public float attackStrength;
    public float knockbackStrength;
    public List<GenericEnemy> hitEnemies;

    private void Start()
    {
        // Get the playerScript component from the player object
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        hitEnemies.Add(collision.gameObject.GetComponent<GenericEnemy>());
    }

    private void HitEnemy()
    {
        // Code to handle the behavior of the projectile upon hitting the enemy
        // For example, you can destroy the projectile gameObject or apply some visual effect
        Destroy(gameObject);

        // Access playerScript and perform actions based on the attack
        if (player != null)
        {
            // Example: Increase player's attackMultiplier
            // player.IncreaseAttackMultiplier(attackMultiplier);
        }
    }
}
