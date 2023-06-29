using System;
using Combat.Weapon.Base;

namespace Combat.HitEffect
{
    public interface IHitNotifier
    {
        event Action<HitInfo> OnHit;
    }
}