using Combat.Hit;
using Extension;
using Player.HookController;
using Unit.Ragdoll;
using UnityEngine;

namespace Combat.Damageable.HitListener
{
    public class RagdollOnHookHit : MonoBehaviour, IHitListener
    {
        private IRagdoll _ragdoll;

        private void Awake()
        {
            _ragdoll = gameObject.RequireComponent<IRagdoll>();
        }
        
        public void OnHit(ref HitInfo hitInfo)
        {
            if(!hitInfo.TryGetAdditionalInfo(out HookInfo hookInfo)) return;
            _ragdoll.Activate();
        }
    }
}