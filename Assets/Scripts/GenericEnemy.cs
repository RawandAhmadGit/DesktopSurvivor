using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    //fill this with all kinds of enemies
    undefined = 0,
    worm1,
    worm2,
    worm3,
    virus1,
    virus2,
    virus3,
    bacteria1,
    bacteria2,
    bacteria3,
    loveLetter1,
    loveLetter2,
    loveLetter3,
    trojan1,
    trojan2,
    trojan3,
    skull1,
    skull2,
    skull3
};


public class GenericEnemy : MonoBehaviour
{
    private GameObject thePlayer;
    public GameObject damageNumbersPrefab;
    private float speed = 1f;
    private EnemyType _type = EnemyType.undefined;
    private float _maxHP = 40;
    public float _currentHP = 40;
    private float MissingHP() { return _maxHP - _currentHP; }
    public float PercentageHP() { return _currentHP / _maxHP; }
    private float _attackStrength = 8;
    public  float GetAttackStrength() { return _attackStrength; }
    private float _attackCooldown;
    private float _expYield = 2;
    private float _knockbackMultiplier = 1;
    private bool _isDead = false;
    private bool hasBeenHit = false;
    private Vector3 currentPosition; // Variable to store the enemy's current position

    
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = thePlayer.GetComponent<Transform>().position;
        Vector3 connectionLine = target - gameObject.transform.position;
        connectionLine.Normalize();
        connectionLine *= speed * Time.deltaTime;
        if (_isDead) connectionLine *= -1; //if the enemy is dead, they move away from the player instead
        gameObject.transform.Translate(connectionLine);
        if (_isDead)
        {
            //any updates that have to be done while the enemy is dead but not yet deleted (aka death animation)
        } 
        else
        {
            _attackCooldown -= Time.deltaTime;

            //TODO perish

        }
    }


    public void defineEnemyType(EnemyEntry incomingData, bool isBoss)
    {

        this._attackStrength = incomingData.attack;
        this._currentHP = incomingData.hp;
        this._maxHP = incomingData.hp;
        this.speed = incomingData.speed;
        this._expYield = incomingData.xp;
        this._knockbackMultiplier = incomingData.knockback;
        gameObject.GetComponent<Animator>().Play(incomingData.name);
        if (isBoss)
        {
            _maxHP *= 10;
            _knockbackMultiplier *= 0.5f;
            _attackStrength *= 2f;
            _expYield *= 20;
            gameObject.transform.localScale *= 1.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead) return;

        if (collision.gameObject.CompareTag("PlayerAttack")) //if the collided object has the tag playerAttack, do damage taking stuff
            {
                //the following code won't compile unless the script "player Attack" has been created. TODO
                TakeDamage(collision.gameObject.GetComponent<playerAttack>().attackStrength);
                TakeKnockback(collision.gameObject.GetComponent<playerAttack>().knockbackStrength);
            }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 otherDude = collision.transform.position;
            gameObject.transform.Translate(2 * Time.deltaTime * (gameObject.transform.position - otherDude).normalized);

            // Store the hit enemy in the bullet script to prevent further collisions with the same enemy
            collision.gameObject.GetComponent<playerAttack>().RegisterHitEnemy(this);
        }
    }
   
    
    private void TakeDamage(float incomingDamage)
    {
        this._currentHP -=     incomingDamage;
        if (_currentHP < 0)
        {
            _currentHP = 0;
            die();
        }
        
        // Show the damage numbers
        // ShowDamageNumbers(incomingDamage);
    }

    /*private void ShowDamageNumbers(float damage)
    {
        // Instantiate the damage numbers GameObject
        GameObject damageNumbersObject = Instantiate(damageNumbersPrefab, currentPosition + new Vector3(0f, 1f, 0f), Quaternion.identity);

        // Get the DamageNumbers component from the instantiated damage numbers GameObject
        DamageNumbers damageNumbers = damageNumbersObject.GetComponent<DamageNumbers>();

        // Display the damage on the damage numbers
        damageNumbers.ShowDamage(damage);
    }*/

    private void die()
    {
        //TODO
    }

    private void TakeKnockback(float incomingKnockback)
    {
        Vector2 knockbackDirection = (transform.position - thePlayer.transform.position).normalized;

        // Apply the knockback force
        //BERNI ZEIGEN
        transform.Translate((Vector3)knockbackDirection * incomingKnockback * _knockbackMultiplier);
    }

    public void MeleeAttack()
    {
        this._attackCooldown = 1;
    }

    public bool CanAttack(playerScript playerScript)
    {
        return this._attackCooldown <= 0;
    }

    public bool HasBeenHitByWeapon()
    {
        return hasBeenHit;
    }

    public void RegisterHitByWeapon()
    {
        hasBeenHit = true;
    }
}
