using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;

public class playerScript : MonoBehaviour
{
    private const float baseMoveSpeed = 3f;
    private const float minimumSpeed = 1f;
    private float attackMultiplier = 1;
    public float attackspeedModifier = 1;
    private float critDamage = 2;
    private float critRate = 1/16;
    private float debuffMoveSpeedMultiplier = 1f;
    private float knockbackMultiplier = 1;
    private float moveSpeedStatMultiplier = 1f;
    public float projectilecountModifier = 0;
    private float projectiledurationModifier = 1;
    private float projectilesizeModifier = 1;
    private float projectilespeedModifier = 1;
    private List<HeldWeapon> heldWeapons = new();
    public DataHolder dataHolder;
    public UnityEngine.GameObject prefab_CDROM;


    private float EffectiveSpeed()
    {
        return math.max(baseMoveSpeed * moveSpeedStatMultiplier * debuffMoveSpeedMultiplier, minimumSpeed);
    }

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        heldWeapons.Add(new HeldWeapon(dataHolder.getWeaponEntry(weapontype.CDRom, 1),this));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 _frameAccel;
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
        foreach (HeldWeapon w in heldWeapons)
        {
            if (w.wData.type == weapontype.CDRom)
            {
                w.Update(prefab_CDROM);
            }
        }
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
