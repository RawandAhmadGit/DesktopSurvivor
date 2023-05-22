using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public float knockbackStrength;
    public float attackStrength;
    public float attackMultiplier;
    public float attackspeedModifier;
    public float projectilespeedModifier;
    public float projectilesizeModifier;
    public float projectilecountModifier;
    public float critRate;
    public float critDamage;

    private playerScript player; // Reference to the playerScript

    private void Start()
    {
        // Get the playerScript component from the player object
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GenericEnemy enemy = collision.GetComponent<GenericEnemy>();

        if (enemy != null && !enemy.HasBeenHitByWeapon(this))
        {
            enemy.takeKnockback(knockbackStrength);
            enemy.takeDamage(attackStrength);
            enemy.RegisterHitByWeapon(this);
            HitEnemy();
        }
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
            player.IncreaseAttackMultiplier(attackMultiplier);
        }
    }
}
