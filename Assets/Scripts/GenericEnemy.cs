using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum EnemyType {
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


public class GenericEnemy : MonoBehaviour {
    private GameObject thePlayer;
    // Damage Numbers
    public GameObject damageNumbersPrefab;
    private float displayDuration = 0.25f;
    private float fadeSpeed = 10f;
    private Vector2 offset = new Vector2(0.2f, -0.15f);
    // Enemy Movement
    private float speed = 1f;
    private EnemyType _type = EnemyType.undefined;
    private float _maxHP = 40;
    public float _currentHP = 40;
    private float MissingHP() { return _maxHP - _currentHP; }
    public float PercentageHP() { return _currentHP / _maxHP; }
    private float _attackStrength = 8;
    public float GetAttackStrength() { return _attackStrength; }
    private float _attackCooldown;
    private float _expYield = 2;
    private float _knockbackMultiplier = 1;
    private bool _isDead = false;
    private bool hasBeenHit = false;
    private Vector3 currentPosition; // Variable to store the enemy's current position
    private float destructionTimer = 1;


    // Start is called before the first frame update
    void Start() {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (!_isDead) {
            Vector3 target = thePlayer.GetComponent<Transform>().position;
            Vector3 connectionLine = target - gameObject.transform.position;
            connectionLine.Normalize();
            connectionLine *= speed * Time.deltaTime;
            if (_isDead) connectionLine *= -1; //if the enemy is dead, they move away from the player instead
            gameObject.transform.Translate(connectionLine);
            _attackCooldown -= Time.deltaTime;
        } else {
            destructionTimer -= Time.deltaTime;
            transform.Rotate(0, 0, 360 * Time.deltaTime);
            transform.localScale = new Vector3(
                transform.localScale.x - Time.deltaTime,
                transform.localScale.y - Time.deltaTime,
                transform.localScale.z);
            if (destructionTimer < 0) {
                Destroy(gameObject);
            }
        }
    }


    public void defineEnemyType(EnemyStatTupel incomingData, bool isBoss) {

        this._attackStrength = incomingData.attack;
        this._currentHP = incomingData.hp;
        this._maxHP = incomingData.hp;
        this.speed = incomingData.speed;
        this._expYield = incomingData.xp;
        this._knockbackMultiplier = incomingData.knockback;
        gameObject.GetComponent<Animator>().Play(incomingData.name);
        if (isBoss) {
            _maxHP *= 10;
            _knockbackMultiplier *= 0.5f;
            _attackStrength *= 2f;
            _expYield *= 20;
            gameObject.transform.localScale *= 1.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_isDead) return;

        if (collision.gameObject.CompareTag("PlayerAttack")) //if the collided object has the tag playerAttack, do damage taking stuff
        {
            PlayerAttack pa = collision.gameObject.GetComponent<PlayerAttack>();
            if (!collision.GetComponent<PlayerAttack>().IsRegistered(this)) {
                Debug.Log("I got hit by a player attack");
                TakeDamage(pa.wData.damage);
                TakeKnockback(pa.wData.knockback);
                pa.RegisterHitEnemy(this);
                GameObject newDmgNumber = Instantiate(damageNumbersPrefab, transform.position, Quaternion.identity);
                //for some reason I cant get past this ^ instantiate
                newDmgNumber.GetComponent<TextMesh>().text = pa.wData.damage.ToString();
                pa.remainingHits--;
            }
        }
    }


    private void TakeDamage(float incomingDamage) {
        this._currentHP -= incomingDamage;
        if (_currentHP <= 0) {
            _currentHP = 0;
            die();
        }


        // Show the damage numbers
        //ShowDamageNumbers(incomingDamage);
    }





    private void die() {
        _isDead = true;
        thePlayer.GetComponent<PlayerScript>().GetXp(_expYield);
    }

    private void TakeKnockback(float incomingKnockback) {
        Vector2 knockbackDirection = (transform.position - thePlayer.transform.position).normalized;
        transform.Translate((Vector3)knockbackDirection * incomingKnockback * _knockbackMultiplier);
    }

    public void MeleeAttack() {
        this._attackCooldown = 1;
    }

    public bool CanAttack(PlayerScript playerScript) {
        return this._attackCooldown <= 0;
    }

    public bool HasBeenHitByWeapon() {
        return hasBeenHit;
    }

    public void RegisterHitByWeapon() {
        hasBeenHit = true;
    }
}
