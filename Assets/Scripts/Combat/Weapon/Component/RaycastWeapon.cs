using System;
using System.Linq;
using Combat.Hit;
using Combat.Weapon.Base;
using Combat.Weapon.HitListener;
using UnityEngine;

namespace Combat.Weapon.Component
{
    public class RaycastWeapon : BaseWeapon, IFireNotifier
    {
        private const float RAYCAST_RADIUS = 0.2f;
        [SerializeField] private Transform _raycastOrigin;
        [SerializeField] private float _maxDistance = 10;
        [SerializeField] private MonoBehaviour[] _hitListeners;

        public event Action OnFire;

        public override void Fire(Action<HitInfo> hitCallback)
        {
            OnFire?.Invoke();
            var info = GetHitInfo();
            hitCallback?.Invoke(info);
        }

        private HitInfo GetHitInfo()
        {
            var ray = new Ray(_raycastOrigin.position, _raycastOrigin.forward);
            Physics.SphereCast(ray, RAYCAST_RADIUS, out var hit, _maxDistance);
            var hitInfo = HitInfo.FromRaycastHit(hit);
            hitInfo.NotifyListeners(_hitListeners.Select(it => it as IHitListener));
            return hitInfo;
        }
    }
}
