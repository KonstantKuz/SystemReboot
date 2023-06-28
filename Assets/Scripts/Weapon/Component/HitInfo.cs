using Common;
using Damageable;
using JetBrains.Annotations;
using UnityEngine;

namespace Weapon.Component
{
    public class HitInfo
    {
        [CanBeNull]
        public GameObject RootGameObject;
        [CanBeNull]
        public RaycastHit? RaycastHit;

        public static HitInfo FromRaycastHit(RaycastHit raycastHit)
        {
            if (raycastHit.collider == null) return null;
         
            var root = raycastHit.collider.GetComponentInParent<IObjectRoot>();
            return new HitInfo
            {
                RootGameObject = root?.Root ?? raycastHit.collider.gameObject,
                RaycastHit = raycastHit,
            };
        }
    }

    public static class HitInfoExtension
    {
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
    }
}