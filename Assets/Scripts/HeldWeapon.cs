using Mono.Cecil;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

internal class HeldWeapon
{
    public playerScript holder;
    public float remainingCooldown;
    public WeaponEntry wData;

    public HeldWeapon(WeaponEntry entry, playerScript holder)
    {
        wData = entry;
        this.holder = holder;
        remainingCooldown = 0;
    }

    public void Update()
    {
        remainingCooldown -= Time.deltaTime;
        if (remainingCooldown < 0)
        {
            Fire();
            remainingCooldown = GetNextCooldown();
        }
    }

    private void Fire()
    {
        for (int i = 0; i<wData.projectileCount + holder.projectilecountModifier; i++)
        {
            GameObject newProjectile = new();
            newProjectile.AddComponent<Image>();
            playerAttack refPA = newProjectile.AddComponent<playerAttack>();
            refPA.attackStrength = wData.damage;
            if (Random.Range(0,1) < holder.critRate) refPA.attackStrength *= holder.critDamage;
            refPA.knockbackStrength = wData.knockback;
            refPA.projectileDuration = wData.maxDuration;
            newProjectile.transform.position = holder.transform.position;
            switch (wData.type)
            {
                //this needs to add the specific components and Images.
                default:
                    break;
            }
        }
        return;
    }

    public float GetNextCooldown()
    {
        return 60/wData.fireRate/holder.attackspeedModifier;
    }
}