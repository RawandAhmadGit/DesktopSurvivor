using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

public class playerScript : MonoBehaviour
{
    public const float baseMoveSpeed = 3f;
    public const float minimumSpeed = 1f;
    public float attackMultiplier;
    public float attackspeedModifier;
    public float attackStrength;
    public float critDamage;
    public float critRate;
    public float debuffMoveSpeedMultiplier = 1f;
    public float knockbackStrength;
    public float moveSpeedStatMultiplier = 1f;
    public float projectilecountModifier;
    public float projectiledurationModifier;
    public float projectilesizeModifier;
    public float projectilespeedModifier;
    private List<HeldWeapon> heldWeapons;
    public DataHolder dataHolder;

    private float EffectiveSpeed()
    {
        return math.max(baseMoveSpeed * moveSpeedStatMultiplier * debuffMoveSpeedMultiplier, minimumSpeed);
    }
    private Vector2 _frameAccel;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _frameAccel = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _frameAccel.y += EffectiveSpeed() * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _frameAccel.y -= EffectiveSpeed() * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _frameAccel.x += EffectiveSpeed() * Time.deltaTime;
            _spriteRenderer.flipX = false; // face right
        }

        if (Input.GetKey(KeyCode.A))
        {
            _frameAccel.x -= EffectiveSpeed() * Time.deltaTime;
            _spriteRenderer.flipX = true; // face left
        }

        Vector2 controllerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (controllerInput.magnitude > 0.1f)
        {
            Vector2 inputDirection = controllerInput.normalized;
            _frameAccel = inputDirection * EffectiveSpeed() * Time.deltaTime;
            if (controllerInput.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (controllerInput.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        gameObject.transform.Translate(_frameAccel);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GenericEnemy genericEnemy = collision.gameObject.GetComponent<GenericEnemy>();
            if (genericEnemy.CanAttack(this))
            {
                genericEnemy.MeleeAttack();
                this.takeDamage(genericEnemy.GetAttackStrength());
            }
        }
    }

    private void takeDamage(float value)
    {
        print("ouch!");
        //TODO
    }
}
