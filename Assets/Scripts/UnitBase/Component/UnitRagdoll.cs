using System.Collections;
using Ragdoll;
using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace UnitBase.Component
{
    public class UnitRagdoll : MonoBehaviour, IRagdoll
    {
        private Animator _animator;
        private Rigidbody[] _rigidbodies;

        private void Awake()
        {
            _animator = gameObject.GetComponentInChildren<Animator>();
            _rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            _rigidbodies.ForEach(it => it.isKinematic = true);
        }

        public void Activate()
        {
            if(_animator == null) return;
            _animator.enabled = false;
            _rigidbodies.ForEach(SetAnimatorVelocity);
        }

        private void SetAnimatorVelocity(Rigidbody rigidbody)
        {
            rigidbody.isKinematic = false;
            rigidbody.velocity = _animator.velocity;
            rigidbody.angularVelocity = _animator.angularVelocity;
        }
        
        private IEnumerator SmoothDepenetration()
        {
            var originVelocities = new float[_rigidbodies.Length];
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                originVelocities[i] = _rigidbodies[i].maxDepenetrationVelocity;
                _rigidbodies[i].maxDepenetrationVelocity = 0.1f;
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                if(_rigidbodies[i] == null) continue;
                _rigidbodies[i].maxDepenetrationVelocity = originVelocities[i];
            }
        }
    }
}