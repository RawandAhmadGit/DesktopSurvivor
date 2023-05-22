using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum enemyType
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
    trojan1,
    trojan2,
    trojan3,
    skull1,
    skull2,
    skull3
};


public class GenericEnemy : MonoBehaviour
{
    public GameObject thePlayer;
    private float speed = 1f;
    private enemyType _type = enemyType.undefined;
    private float _maxHP = 40;
    private float _currentHP = 40;
    private float _missingHP() { return _maxHP - _currentHP; }
    public float percentageHP() { return _currentHP / _maxHP; }
    private float _attackStrength = 8;
    public  float getAttackStrength() { return _attackStrength; }
    private float _attackCooldown;
    private float _expYield = 2;
    private float _knockbackMultiplier = 1;
    private bool _isDead = false;

    
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
        connectionLine.Scale(new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed));
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

    void defineEnemyType(enemyType newType, bool isBoss)
    {
        _type = newType; //currently not doing anything :( TODO
        _maxHP = 20;
        if (isBoss) _maxHP *= 10;
        _currentHP = _maxHP;
        _attackStrength = 8;
        if (isBoss) _attackStrength *= 2;
        speed = 1;
        _expYield = 2;
        if (isBoss) _expYield *= 20;
        _knockbackMultiplier = 1;
        if (isBoss) _knockbackMultiplier /= 2;
        //TODO
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead) return;

        if (collision.gameObject.CompareTag("PlayerAttack")) //if the collided object has the tag playerAttack, do damage taking stuff
            {
                //the following code won't compile unless the script "player Attack" has been created. TODO
                takeDamage(collision.gameObject.GetComponent<playerAttack>().strength);
                takeKnockback(collision.gameObject.GetComponent<playerAttack>().knockback);
            }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 otherDude = collision.transform.position;
            gameObject.transform.Translate((gameObject.transform.position - otherDude).normalized * Time.deltaTime * 2);
        }
    }
   
    
    private void takeDamage(float incomingDamage)
    {
        this._currentHP -=     incomingDamage;
        if (_currentHP < 0)
        {
            _currentHP = 0;
            die();
        }

    }
    private void die()
    {
        //TODO
    }
    private void takeKnockback(float incomingKnockback)
    {
        //TODO
    }

    public void meleeAttack()
    {
        this._attackCooldown = 1;
    }

    public bool canAttack(playerScript playerScript)
    {
        return this._attackCooldown <= 0;
    }
}
