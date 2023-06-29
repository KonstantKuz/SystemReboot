using Combat.Weapon.Base;

namespace Combat.Damageable
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