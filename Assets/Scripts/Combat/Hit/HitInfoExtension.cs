using System.Collections.Generic;
using Combat.Damageable;
using Combat.Weapon.HitListener;
using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Combat.Hit
{
    public static class HitInfoExtension
    {
        public static bool IsEmpty(this HitInfo hitInfo)
        {
            return hitInfo.RootGameObject == null && hitInfo.RaycastHit == null;
        }

        public static bool IsCritical(this HitInfo hitInfo)
        {
            return hitInfo.TryGetAdditionalInfo(out IsCriticalInfo isCritical) && isCritical.Value;
        }
        
        public static bool TryGetRootDamageable(this HitInfo hitInfo, out IDamageable damageable)
        {
            damageable = hitInfo.RootGameObject ? hitInfo.RootGameObject.GetComponent<IDamageable>() : null;
            return damageable != null;
        }

        public static bool TryGetRigidbody(this HitInfo hitInfo, out Rigidbody rigidbody, int searchDepth = 2)
        {
            if (hitInfo.RaycastHit == null)
            {
                rigidbody = null;
                return false;
            }

            var collider = hitInfo.RaycastHit.Value.collider;
            if (collider.TryGetComponent(out rigidbody))
            {
                return true;
            }

            var parent = collider.transform.parent;
            for (int i = 0; i < searchDepth; i++)
            {
                if (parent.TryGetComponent(out rigidbody))
                {
                    return true;
                }
                parent = parent.parent;
            }

            return false;
        }

        public static void NotifyListeners(this ref HitInfo hitInfo, IEnumerable<IHitListener> listeners)
        {
            foreach (var hitModifier in listeners)
            {
                hitModifier.OnHit(ref hitInfo);
            }
        }
    }
}