using Combat.Hit;
using Ragdoll.Sliceable;
using UnityEngine;

namespace Combat.Weapon.HitListener
{
    public class AddSliceInfo : MonoBehaviour, IHitListener
    {
        [SerializeField] private Transform _sliceConeOrigin;
        [SerializeField] private float _force;

        public void OnHit(ref HitInfo hitInfo)
        {
            var sliceParams = SliceInfo.Create(_sliceConeOrigin, _force);
            hitInfo.AppendInfo(sliceParams);
        }
    }
}