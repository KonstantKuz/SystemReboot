
namespace Combat.Hit
{
    public struct DamageInfo
    {
        public int Damage { get; private set; }
        public static DamageInfo Create(int damage) => new DamageInfo { Damage = damage};
    }
}