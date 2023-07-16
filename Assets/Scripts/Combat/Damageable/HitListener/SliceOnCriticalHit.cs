using Combat.Hit;
using Extension;
using Ragdoll.Sliceable;
using UnityEngine;

namespace Combat.Damageable.HitListener
{
    public class SliceOnCriticalHit : MonoBehaviour, IHitListener
    {
        private ISliceable _sliceable;
        
        private void Awake()
        {
            _sliceable = gameObject.RequireComponentInChildren<ISliceable>();
        }

        public void OnHit(ref HitInfo hitInfo)
        {
            if(!hitInfo.IsCritical() || !hitInfo.TryGetAdditionalInfo(out SliceInfo sliceParams)) return;
            _sliceable.Slice(sliceParams);
        }
    }
}