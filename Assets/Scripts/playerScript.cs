using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    private float currentHP = 64;
    private bool isDead = false;
    private float maxHP = 64;
    [SerializeField]
    private int xp = 0;
    [SerializeField]
    private int level = 1;
    private float defense = 0;
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
    public GameObject prefab_LEVEL_UP;
    public UnityEngine.GameObject prefab_CDROM;
    public GameObject prefab_Firewall;
    public GameObject prefab_Loading_Icon;
    public GameObject prefab_Resizer;
    public GameObject prefab_mousePointer;

    public UnityEvent playerDeath;


    private float EffectiveSpeed()
    {
        return math.max(baseMoveSpeed * moveSpeedStatMultiplier * debuffMoveSpeedMultiplier, minimumSpeed);
    }

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GainWeapon(weapontype.FireWall);
        playerDeath.AddListener(Die);
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
            w.Update();
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
        value = value - defense;
        currentHP -= value;
        if (currentHP <= 0)
        {
            playerDeath.Invoke();
        }
    }

    internal void GetXp(float expYield)
    {
        this.xp += (int)expYield;
        while (xp >= xpNeeded())
        {
            xp -= xpNeeded();
            level++;
            Instantiate(prefab_LEVEL_UP, transform.position, quaternion.identity);
            GainWeapon(weapontype.CDRom);
        }
    }

    private int xpNeeded()
    {
        return 8 + (2 * level);
    }

    public void GainWeapon(weapontype type)
    {
        if (heldWeapons.Count >= 6) return;
        if (heldWeapons.Count == 0){
            heldWeapons.Add(new HeldWeapon(DS_Data.GetWeaponEntry(type, 1), this, GetPrefabForType(type))); 
            return; 
        }
        foreach (HeldWeapon w in heldWeapons)
        {
            if(w.wData.type == type && w.wData.level < 8)
            {
                w.wData = DS_Data.GetWeaponEntry(type, w.wData.level + 1);
                return;
            }
        }
        heldWeapons.Add(new HeldWeapon(DS_Data.GetWeaponEntry(type, 1), this, GetPrefabForType(type)));
    }

    private GameObject GetPrefabForType(weapontype type)
    {
        switch (type)
        {
            case weapontype.CDRom:          return prefab_CDROM;
            case weapontype.FireWall:       return prefab_Firewall;
            case weapontype.LoadingIcon:    return prefab_Loading_Icon;
            case weapontype.MousePointer:   return prefab_mousePointer;
            case weapontype.Resizer:        return prefab_Resizer;
        }
        return null;
    }

    private void Die()
    {
        heldWeapons.Clear();
        isDead = true;
        moveSpeedStatMultiplier = 0;
        for (int i = 0; i < 100; i++)
        {
            Instantiate(prefab_CDROM,transform.position,quaternion.identity);
        }
    }
}
