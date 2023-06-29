using System;
using System.Collections.Generic;
using System.Linq;
using Combat.HitEffect;
using Combat.Weapon.Base;
using Common;
using Extension;
using Ragdoll.Sliceable;
using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Combat.Weapon.Component
{
    public class SlicingWeapon : BaseWeapon, IHitNotifier
    {
        [SerializeField] private Transform _sliceConeOrigin;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _force;

        public event Action<HitInfo> OnHit;

        public override void Fire(Action<HitInfo> hitCallback)
        {
            var hits = Physics.OverlapSphere(_sliceConeOrigin.position, _maxDistance)
                .Where(it => IsInAttackRange(it.transform.position));
            SliceHits(hits);
            NotifyHits(hits, hitCallback);
        }

        private void SliceHits(IEnumerable<Collider> hits)
        {
            var sliceables = hits.Select(it => it.GetComponentInParent<ISliceable>())
                .Where(it => it != null)
                .ToHashSet();
            sliceables.ForEach(Slice);
        }

        private void NotifyHits(IEnumerable<Collider> hits, Action<HitInfo> hitCallback)
        {
            var rootHitInfos = hits.Select(it => it.GetComponentInParent<IObjectRoot>())
                .Where(it => it != null)
                .ToHashSet()
                .Select(it => new HitInfo {RootGameObject = it.Root});
            
            rootHitInfos.ForEach(it =>
            {
                hitCallback?.Invoke(it);
                OnHit?.Invoke(it);
            });
        }

        private bool IsInAttackRange(Vector3 point)
        {
            return point.IsInsideCone(_sliceConeOrigin.position, _sliceConeOrigin.forward, _maxAngle, 0f, _maxDistance);
        }
        
        private void Slice(ISliceable sliceable)
        {
            var sliceParams = new SliceParams
            {
                Plane = new Plane(_sliceConeOrigin.up, _sliceConeOrigin.position),
                Force = _sliceConeOrigin.forward * _force,
            };
            sliceable.Slice(sliceParams);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(_sliceConeOrigin.position, _sliceConeOrigin.position + Quaternion.Euler(0,_maxAngle / 2,0) *_sliceConeOrigin.forward * _maxDistance);
            Gizmos.DrawLine(_sliceConeOrigin.position, _sliceConeOrigin.position + Quaternion.Euler(0,-_maxAngle / 2,0) *_sliceConeOrigin.forward * _maxDistance);
        }
    }
}
