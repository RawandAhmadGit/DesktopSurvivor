using Mono.Cecil;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

internal class HeldWeapon
{
    public playerScript holder;
    public float remainingCooldown;
    public WeaponStatsTupel wData;
    private GameObject Prefab;

    public HeldWeapon(WeaponStatsTupel tupel, playerScript holder, GameObject Prefab)
    {
        wData = tupel;
        this.holder = holder;
        remainingCooldown = 1;
        this.Prefab = Prefab;
    }

    public void Update()
    {
        remainingCooldown -= Time.deltaTime;
        if (remainingCooldown == float.PositiveInfinity)
            remainingCooldown = GetNextCooldown();

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

            UnityEngine.GameObject newObject = UnityEngine.GameObject.Instantiate(Prefab, holder.transform.position, Quaternion.identity);
            newObject.GetComponent<PlayerAttack>().wData = this.wData;
        }
        return;
    }

    public float GetNextCooldown()
    {
        return 60/wData.fireRate/holder.attackspeedModifier;
    }
}