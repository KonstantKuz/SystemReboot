using System;
using UnityEngine;

namespace Weapon.Component
{
    public class RaycastWeapon : BaseWeapon, IFireNotifier, IHitNotifier
    {
        [SerializeField] private Transform _raycastOrigin;
        [SerializeField] private float _maxDistance = 10;

        public event Action OnShoot;
        public event Action<HitInfo> OnHit;

        public override void Fire(Action<HitInfo> hitCallback)
        {
            Physics.Raycast(_raycastOrigin.position, _raycastOrigin.forward, out var hit, _maxDistance);
            var info = HitInfo.FromRaycastHit(hit);
            hitCallback?.Invoke(info);
            OnShoot?.Invoke();
            OnHit?.Invoke(info);
        }
    }
}
