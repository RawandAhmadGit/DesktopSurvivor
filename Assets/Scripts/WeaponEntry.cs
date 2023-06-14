public enum weapontype
{
    MousePointer,
    CDRom,
    Resizer,
    LoadingIcon,
    Firewall
}

public class WeaponEntry
{
    public weapontype type;
    const int colWeapontype = 0;
    public int level;
    const int colLevel = 1;
    public string desc;
    const int colDesc = 2;
    public float damage;
    const int colDamage = 3;
    public float fireRate;
    const int colFireRate = 4;
    public int maxTargets;
    const int colMaxTargets = 5;
    public float projectileSpeed;
    const int colProjectileSpeed = 6;
    public int projectileCount;
    const int colProjectileCount = 7;
    public float maxDuration;
    const int colMaxDuration = 8;
    public float knockback;
    const int colKnockback = 9;
    public float size1;
    const int colSize1 = 10;
    public float size2;
    const int colSize2 = 11;
}