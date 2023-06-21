internal class HeldWeapon
{
    public playerScript holder;
    public float remainingCooldown;
    public int level;
    public WeaponEntry wData;
    public float GetNextCooldown()
    {
        remainingCooldown = 10;
        return 60/wData.fireRate/holder.attackspeedModifier;
    }
}