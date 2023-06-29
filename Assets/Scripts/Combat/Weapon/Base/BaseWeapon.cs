using System;
using UnityEngine;

namespace Combat.Weapon.Base
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public abstract void Fire(Action<HitInfo> hitCallback);
    }
}
