using System;
using System.Collections.Generic;
using System.Linq;
using Extension;
using Ragdoll.Sliceable;
using SuperMaxim.Core.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapon.Component.Slicing
{
    public class SlicingWeapon : BaseWeapon
    {
        [SerializeField] private Transform _sliceConeOrigin;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _force;

        public override void Fire(Action<GameObject> hitCallback)
        {
            var hits = Physics.OverlapSphere(_sliceConeOrigin.position, _maxDistance);
            var sliceables = hits.Where(it => IsInAttackRange(it.transform.position))
                .Select(it => it.GetComponentInParent<ISliceable>())
                .Where(it => it != null);
            var hashSet = new HashSet<ISliceable>(sliceables);
            hashSet.ForEach(Slice);
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
