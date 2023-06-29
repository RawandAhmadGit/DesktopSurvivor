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
        remainingCooldown = 1;
    }

    public void Update(UnityEngine.GameObject incomingPrefab)
    {
        remainingCooldown -= Time.deltaTime;
        if (remainingCooldown == float.PositiveInfinity)
            remainingCooldown = GetNextCooldown();

        if (remainingCooldown < 0)
        {
            Fire(incomingPrefab);
            remainingCooldown = GetNextCooldown();
        }
    }

    private void Fire(UnityEngine.GameObject incoming)
    {
        for (int i = 0; i<wData.projectileCount + holder.projectilecountModifier; i++)
        {

            UnityEngine.GameObject newObject = UnityEngine.GameObject.Instantiate(incoming, holder.transform.position, Quaternion.identity);
            newObject.GetComponent<playerAttack>().wData = this.wData;
        }
        return;
    }

    public float GetNextCooldown()
    {
        return 60/wData.fireRate/holder.attackspeedModifier;
    }
}