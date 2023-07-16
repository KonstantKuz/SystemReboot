using System;
using System.Collections.Generic;
using System.Linq;
using Combat.Hit;
using Combat.Weapon.Base;
using Combat.Weapon.HitListener;
using Common;
using Extension;
using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Combat.Weapon.Component
{
    public class MeleeWeapon : BaseWeapon
    {
        [SerializeField] private Transform _damageConeOrigin;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _maxAngle;
        [SerializeField] private MonoBehaviour[] _hitListeners;
        
        public override void Fire(Action<HitInfo> hitCallback)
        {
            var hits = Physics.OverlapSphere(_damageConeOrigin.position, _maxDistance)
                .Where(it => IsInAttackRange(it.transform.position));
            NotifyHits(hits, hitCallback);
        }

        private void NotifyHits(IEnumerable<Collider> hits, Action<HitInfo> hitCallback)
        {
            var rootHitInfos = hits.Select(it => it.GetComponentInParent<IObjectRoot>())
                .Where(it => it != null)
                .ToHashSet()
                .Select(it =>
                {
                    var info = new HitInfo {RootGameObject = it.Root};
                    info.NotifyListeners(_hitListeners.Select(it => it as IHitListener));
                    return info;
                });
    
            rootHitInfos.ForEach(it =>
            {
                hitCallback?.Invoke(it);
            });
        }

        private bool IsInAttackRange(Vector3 point)
        {
            return point.IsInsideCone(_damageConeOrigin.position, _damageConeOrigin.forward, _maxAngle, 0f, _maxDistance);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(_damageConeOrigin.position, _damageConeOrigin.position + Quaternion.Euler(0,_maxAngle / 2,0) *_damageConeOrigin.forward * _maxDistance);
            Gizmos.DrawLine(_damageConeOrigin.position, _damageConeOrigin.position + Quaternion.Euler(0,-_maxAngle / 2,0) *_damageConeOrigin.forward * _maxDistance);
        }
    }
}
