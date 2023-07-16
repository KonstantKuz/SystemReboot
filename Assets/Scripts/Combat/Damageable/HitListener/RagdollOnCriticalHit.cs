using Combat.Hit;
using Extension;
using Unit.Ragdoll;
using UnityEngine;

namespace Combat.Damageable.HitListener
{
    public class RagdollOnCriticalHit : MonoBehaviour, IHitListener
    {
        private IRagdoll _ragdoll;

        private void Awake()
        {
            _ragdoll = gameObject.RequireComponent<IRagdoll>();
        }

        public void OnHit(ref HitInfo hitInfo)
        {
            if(!hitInfo.IsCritical()) return;
            _ragdoll.Activate();
        }
    }
}