using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public PlayerScript player; // Reference to the playerScript
    public WeaponStatsTupel wData;
    public float totalDuration;
    public float currentDuration = 0f;
    public List<GenericEnemy> hitEnemies;
    public List<GenericEnemy> registerBuffer;
    public int remainingHits;
    public int burstPjtlNr;
    public int burstTotalPjtl;

    private void Start() {
        // Get the playerScript component from the player object
        remainingHits = wData.maxTargets;
        if (remainingHits == 0) {
            remainingHits = int.MaxValue;
        }
        totalDuration = wData.maxDuration;
    }

    private void FixedUpdate() {
        while (registerBuffer.Count > 0) {
            hitEnemies.Add(registerBuffer[0]);
            registerBuffer.RemoveAt(0);
        }
    }
    private void Update() {
        // Increase the current duration of the projectile
        currentDuration += Time.deltaTime;

        // Check if the projectile exceeds the maximum duration
        if (currentDuration >= totalDuration) {
            Destroy(gameObject);
        }
        if (remainingHits <= 0) //or has no hits remaining
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Check if the collided object is an enemy
        GenericEnemy enemy = collision.gameObject.GetComponent<GenericEnemy>();
        if (enemy != null) {
            // Handle the enemy hit with attack and knockback
            RegisterHitEnemy(enemy);
        }
    }

    public void RegisterHitEnemy(GenericEnemy enemy) {
        // Store the hit enemy and perform any necessary actions
        registerBuffer.Add(enemy);
    }

    public bool IsRegistered(GenericEnemy enemy) {
        foreach (GenericEnemy e in hitEnemies) {
            if (enemy == e) {
                return true;
            }
        }
        return false;
    }

    public void Unregister(GenericEnemy t) {
        int foundI = -1;
        for (int i = 0; i < hitEnemies.Count; ++i) {
            {
                if (hitEnemies[i] == t) {
                    foundI = i; break;
                }
            }
        }
        if (foundI != -1) {
            hitEnemies.RemoveAt(foundI);
        }
        return;
    }
}
