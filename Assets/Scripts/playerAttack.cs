using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private playerScript player; // Reference to the playerScript
    public WeaponStatsTupel wData;
    public float projectileDuration = 3f;
    public float currentDuration = 0f;
    public List<GenericEnemy> hitEnemies;
    public List<GenericEnemy> registerBuffer;

    private void Start()
    {
        // Get the playerScript component from the player object
    }

    private void FixedUpdate()
    {
        while (registerBuffer.Count > 0)
        {
            hitEnemies.Add(registerBuffer[0]);
            registerBuffer.RemoveAt(0);
        }
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
        }
    }

    public void RegisterHitEnemy(GenericEnemy enemy)
    {
        // Store the hit enemy and perform any necessary actions
        registerBuffer.Add(enemy);
    }

    public bool IsRegistered(GenericEnemy enemy)
    {
        foreach (GenericEnemy e in hitEnemies)
        {
            if (enemy == e)
            {
                return true;
            }
        }
        return false;
    }
}
