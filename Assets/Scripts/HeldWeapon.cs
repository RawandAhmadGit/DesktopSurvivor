internal class HeldWeapon
{
    public playerScript holder;
    public weapontype type;
    public float remainingCooldown;
    public int level;
    public float getNextCooldown()
    {
        remainingCooldown = 10;
        holder.dataHolder.getWeaponEntry(type, level);
    }
}