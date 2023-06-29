using Combat.Damageable;
using Extension;
using UnityEngine;

namespace Unit.Ragdoll
{
    public class RagdollOnDeath : MonoBehaviour
    {
        private Health _health;
        private IRagdoll _ragdoll;

        private void Awake()
        {
            _health = gameObject.RequireComponent<Health>();
            _ragdoll = gameObject.RequireComponent<IRagdoll>();
            _health.OnDeath += EnableRagdoll;
        }

        private void EnableRagdoll(DamageInfo damageInfo)
        {
            _ragdoll.Activate();
        }

        private void OnDestroy()
        {
            _health.OnDeath -= EnableRagdoll;
        }
    }
}