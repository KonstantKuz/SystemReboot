using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Unit.Ragdoll
{
    public class UnitRagdoll : MonoBehaviour, IRagdoll
    {
        private Animator _animator;
        private Rigidbody[] _rigidbodies;
        private AttachedProps _attachedProps;

        private void Awake()
        {
            _animator = gameObject.GetComponentInChildren<Animator>();
            _rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            _attachedProps = gameObject.GetComponentInChildren<AttachedProps>();
            _rigidbodies.ForEach(it => it.isKinematic = true);
        }

        public void Activate()
        {
            if(_animator == null || !_animator.enabled) return;
            _attachedProps?.DetachAll();
            _animator.enabled = false;
            _rigidbodies.ForEach(SetAnimatorVelocity);
        }

        private void SetAnimatorVelocity(Rigidbody rigidbody)
        {
            rigidbody.isKinematic = false;
            rigidbody.velocity = _animator.velocity;
            rigidbody.angularVelocity = _animator.angularVelocity;
        }
    }
}