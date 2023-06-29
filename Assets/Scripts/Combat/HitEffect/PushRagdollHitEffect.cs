using Combat.Weapon.Base;
using Extension;
using Ragdoll;
using Unit.Ragdoll;
using UnityEngine;

namespace Combat.HitEffect
{
    public class PushRagdollHitEffect : MonoBehaviour
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private float _force;
        [SerializeField] private float _verticalForce;
        
        private IHitNotifier _hitNotifier;
        private IHitNotifier HitNotifier => _hitNotifier ??= gameObject.RequireComponent<IHitNotifier>();

        private void Awake()
        {
            HitNotifier.OnHit += OnHit;
        }

        private void OnHit(HitInfo hitInfo)
        {
            if (hitInfo == null || hitInfo.RootGameObject == null) return;
            if(!hitInfo.RootGameObject.TryGetComponent(out IRagdoll ragdoll)) return;
            ragdoll.Activate();
            if (hitInfo.TryGetRigidbody(out var rigidbody))
            {
                rigidbody.velocity = _force * _barrel.transform.forward + _verticalForce * Vector3.up;
            }
        }

        private void OnDestroy()
        {
            HitNotifier.OnHit -= OnHit;
        }
    }
}