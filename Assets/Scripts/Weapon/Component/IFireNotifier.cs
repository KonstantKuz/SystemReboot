using System;

namespace Weapon.Component
{
    public interface IFireNotifier
    {
        event Action OnShoot;
    }
}