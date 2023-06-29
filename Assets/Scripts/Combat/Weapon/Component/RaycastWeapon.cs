using System;
using Combat.HitEffect;
using Combat.Weapon.Base;
using UnityEngine;

namespace Combat.Weapon.Component
{
    public class RaycastWeapon : BaseWeapon, IFireNotifier, IHitNotifier
    {
        private const float RAYCAST_RADIUS = 0.2f;
        [SerializeField] private Transform _raycastOrigin;
        [SerializeField] private float _maxDistance = 10;

        public event Action OnShoot;
        public event Action<HitInfo> OnHit;

        public override void Fire(Action<HitInfo> hitCallback)
        {
            var ray = new Ray(_raycastOrigin.position, _raycastOrigin.forward);
            Physics.SphereCast(ray, RAYCAST_RADIUS, out var hit, _maxDistance);
            var info = HitInfo.FromRaycastHit(hit);
            hitCallback?.Invoke(info);
            OnShoot?.Invoke();
            OnHit?.Invoke(info);
        }
    }
}
