using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCollision : MonoBehaviour
{
    public float invulnerableTime = 1f; // how long the player will be invulnerable after getting hit
    public int maxHealth = 3; // maximum health of the player
    public int currentHealth; // current health of the player
    public bool isInvulnerable; // is the player currently invulnerable?
    public bool isAlive; // is the player currently alive?
    private SpriteRenderer _spriteRenderer; // reference to the player's sprite renderer
    public Text hpCounterText; // reference to the HP counter text object

    private void Awake()
    {
        HPCounter hpCounter = GameObject.FindObjectOfType<HPCounter>(); // get the HPCounter script
        hpCounterText = hpCounter.GetComponent<Text>(); // get the Text component from the HPCounter script
    }

    private void Start()
    {
        currentHealth = maxHealth; // set the initial health to the max health
        isAlive = true; // set the player to be alive at the beginning
        _spriteRenderer = GetComponent<SpriteRenderer>(); // get the sprite renderer component
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Current Health: " + currentHealth);
        if (!isInvulnerable) // check if the player is not invulnerable
        {
            if (collision.CompareTag("Enemy")) // check if the player collided with an enemy
            {
                currentHealth--; // reduce the player's health by 1
                if (currentHealth <= 0) // check if the player's health is zero or less
                {
                    isAlive = false; // set the player to be dead
                }
                else // if the player still has health left
                {
                    isInvulnerable = true; // set the player to be invulnerable
                    StartCoroutine(InvulnerableTime()); // start the invulnerable time coroutine
                }
            }
        }
    }

    private IEnumerator InvulnerableTime()
    {
        for (int i = 0; i < 5; i++) // loop for 5 times
        {
            _spriteRenderer.enabled = false; // hide the player's sprite
            yield return new WaitForSeconds(invulnerableTime / 10); // wait for a short time
            _spriteRenderer.enabled = true; // show the player's sprite
            yield return new WaitForSeconds(invulnerableTime / 10); // wait for a short time
        }
        isInvulnerable = false; // set the player to be vulnerable again
    }
}
