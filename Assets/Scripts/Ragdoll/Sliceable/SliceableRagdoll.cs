using System.Collections;
using BzKovSoft.CharacterSlicer;
using BzKovSoft.ObjectSlicer;
using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Ragdoll.Sliceable
{
    public class SliceableRagdoll : MonoBehaviour, ISliceable
    {
        [SerializeField] private BzSliceableCharacterBase _sliceableCharacter;
        [SerializeField] private float _topPartForce;
        [SerializeField] private float _bottomPartForce;

        [ContextMenu("TestSlice")]
        public void TestSlice()
        {
            var sliceParams = new SliceParams
            {
                Plane = new Plane(Vector3.up, transform.position + Vector3.up),
                Force = -transform.forward * _topPartForce,
            };
            Slice(sliceParams);
        }

        public void Slice(SliceParams sliceParams)
        {
            _sliceableCharacter.Slice(sliceParams.Plane, result => OnSliceComplete(result, sliceParams.Force));
        }

        private void OnSliceComplete(BzSliceTryResult result, Vector3 force)
        {
            if(!result.sliced) return;
            
            var bottomVelocity = force - transform.up * _bottomPartForce / 2;
            var topVelocity = force + transform.up * _topPartForce / 2;
            result.outObjectNeg
                .GetComponentsInChildren<Rigidbody>()
                .ForEach(it => it.velocity = bottomVelocity);
            result.outObjectPos
                .GetComponentsInChildren<Rigidbody>()
                .ForEach(it => it.velocity = topVelocity);
        }

        private IEnumerator ResetVelocity(BzSliceTryResult result, Vector3 force)
        {
            yield return new WaitForFixedUpdate();

            // var bottomVelocity = -transform.forward * _throwForce - transform.up * _throwForce / 2;
            // var topVelocity = -transform.forward * _throwForce + transform.up * _throwForce / 2;
            // result.outObjectNeg
            //     .GetComponentsInChildren<Rigidbody>()
            //     .ForEach(it => it.velocity = bottomVelocity);
            // result.outObjectPos
            //     .GetComponentsInChildren<Rigidbody>()
            //     .ForEach(it => it.velocity = topVelocity);
        }
    }
}