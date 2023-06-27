using System;
using UnityEngine;

namespace Weapon.Component
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public abstract void Fire(Action<GameObject> hitCallback);
    }
}
