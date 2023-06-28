using Weapon.Component;

namespace Damageable
{
    public struct DamageInfo
    {
        public int Damage;
        public HitInfo HitInfo;

        public static DamageInfo Create(int damage, HitInfo hitInfo)
        {
            return new DamageInfo { Damage = damage, HitInfo = hitInfo };
        }
    }
}