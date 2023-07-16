using System.Collections.Generic;
using System.Linq;
using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat.Hit
{
    public struct HitInfo
    {
        private HashSet<object> _additionalInfo;
        private HashSet<object> AdditionalInfo => _additionalInfo ??= new HashSet<object>();

        [CanBeNull]
        public GameObject RootGameObject { get; set; }
        [CanBeNull]
        public RaycastHit? RaycastHit { get; set; }
        
        public static HitInfo FromRaycastHit(RaycastHit raycastHit)
        {
            if (raycastHit.collider == null) return new HitInfo();
         
            var root = raycastHit.collider.GetComponentInParent<IObjectRoot>();
            return new HitInfo
            {
                RootGameObject = root?.Root ?? raycastHit.collider.gameObject,
                RaycastHit = raycastHit,
            };
        }

        public void AppendInfo(object customData)
        {
            AdditionalInfo.Add(customData);
        }

        public bool TryGetAdditionalInfo<T>(out T info)
        {
            var ofType = AdditionalInfo.OfType<T>().ToList();
            if (ofType.Any())
            {
                info = ofType.First();
                return true;
            }
            info = default;
            return false;
        }
    }
}