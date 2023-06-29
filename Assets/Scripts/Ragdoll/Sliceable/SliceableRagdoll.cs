using System;
using BzKovSoft.CharacterSlicer;
using BzKovSoft.ObjectSlicer;
using SuperMaxim.Core.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ragdoll.Sliceable
{
    public class SliceableRagdoll : MonoBehaviour, ISliceable
    {
        [SerializeField] private BzSliceableCharacterBase _sliceableCharacter;
        [SerializeField] private float _topPartForce;
        [SerializeField] private float _bottomPartForce;

        public event Action OnBeforeSlice;

        [ContextMenu("TestSlice")]
        public void TestSlice()
        {
            OnBeforeSlice?.Invoke();
            var sliceParams = new SliceParams
            {
                Plane = new Plane(Vector3.up, transform.position + Vector3.up),
                Force = -transform.forward * _topPartForce,
            };
            Slice(sliceParams);
        }

        public void Slice(SliceParams sliceParams)
        {
            OnBeforeSlice?.Invoke();
            _sliceableCharacter.Slice(sliceParams.Plane, result => OnSliceComplete(result, sliceParams.Force));
        }

        private void OnSliceComplete(BzSliceTryResult result, Vector3 force)
        {
            if(!result.sliced) return;
            
            var bottomVelocity = force - transform.up * _bottomPartForce;
            var topVelocity = force + transform.up * _topPartForce;
            result.outObjectNeg
                .GetComponentsInChildren<Rigidbody>()
                .ForEach(it =>
                {
                    it.velocity += bottomVelocity;
                    it.angularVelocity += Random.onUnitSphere * bottomVelocity.magnitude;
                });
            result.outObjectPos
                .GetComponentsInChildren<Rigidbody>()
                .ForEach(it =>
                {
                    it.velocity += topVelocity;
                    it.angularVelocity += Random.onUnitSphere * topVelocity.magnitude;
                });
        }
    }
}