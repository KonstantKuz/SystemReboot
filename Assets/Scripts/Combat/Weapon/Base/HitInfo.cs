using Combat.Damageable;
using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat.Weapon.Base
{
    public class HitInfo
    {
        [CanBeNull]
        public GameObject RootGameObject;
        [CanBeNull]
        public RaycastHit? RaycastHit;
        [CanBeNull]
        public object CustomData;

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

        public static HitInfo WithCustomData(GameObject root, object data)
        {
            return new HitInfo
            {
                RootGameObject = root,
                CustomData = data,
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