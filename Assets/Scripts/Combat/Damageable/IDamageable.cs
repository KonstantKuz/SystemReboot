using System;
using Combat.Hit;

namespace Combat.Damageable
{
    public interface IDamageable
    {
        event Action<HitInfo> OnDamageTaken;
        void TakeDamage(HitInfo hitInfo);
    }
}