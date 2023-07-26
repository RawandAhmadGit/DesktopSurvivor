using Mono.Cecil;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

internal class HeldWeapon
{
    public PlayerScript holder;
    public float remainingCooldown;
    public WeaponStatsTupel wData;
    private GameObject Prefab;

    public HeldWeapon(WeaponStatsTupel tupel, PlayerScript holder, GameObject Prefab)
    {
        wData = tupel;
        this.holder = holder;
        remainingCooldown = 0;
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
        int burstTotal = wData.projectileCount + holder.projectilecountModifier;
        for (int i = 0; i<burstTotal; i++)
        {

            UnityEngine.GameObject newObject = UnityEngine.GameObject.Instantiate(Prefab, holder.transform.position, Quaternion.identity);
            PlayerAttack newAttack = newObject.GetComponent<PlayerAttack>();
            newAttack.wData = this.wData;
            newAttack.burstPjtlNr = i;
            newAttack.burstTotalPjtl = burstTotal;
        }
        return;
    }

    public float GetNextCooldown()
    {
        return 60/(wData.fireRate * holder.attackspeedModifier);
    }
}