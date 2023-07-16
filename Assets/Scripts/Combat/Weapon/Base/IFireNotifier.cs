using System;

namespace Combat.Weapon.Base
{
    public interface IFireNotifier
    {
        event Action OnFire;
    }
}