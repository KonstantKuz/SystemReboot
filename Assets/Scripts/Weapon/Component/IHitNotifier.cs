using System;

namespace Weapon.Component
{
    public interface IHitNotifier
    {
        event Action<HitInfo> OnHit;
    }
}