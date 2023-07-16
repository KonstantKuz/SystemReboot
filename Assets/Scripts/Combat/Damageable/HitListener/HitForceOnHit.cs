using Combat.Hit;
using UnityEngine;

namespace Combat.Damageable.HitListener
{
    public class HitForceOnHit : MonoBehaviour, IHitListener
    {
        public void OnHit(ref HitInfo hitInfo)
        {
            if(!hitInfo.TryGetAdditionalInfo(out HitForceInfo forceInfo)) return;
            if (hitInfo.TryGetRigidbody(out var rigidbody))
            {
                rigidbody.velocity = forceInfo.Force;
            }
        }
    }
}