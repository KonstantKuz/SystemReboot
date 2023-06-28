using System;
using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Weapon.Component
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public abstract void Fire(Action<HitInfo> hitCallback);
    }

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
    
    public interface IHitResponsive
    {
        void OnHit(HitInfo hitInfo);
    }
}
