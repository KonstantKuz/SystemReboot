using System;
using Combat.Hit;
using Combat.Weapon.Base;
using Common;
using UnityEngine;

namespace Combat.Projectile
{
    public class BaseProjectile : MonoBehaviour
    {
        private Action<HitInfo> HitCallback;
        
        public virtual void Launch(Action<HitInfo> hitCallback)
        {
            HitCallback = hitCallback;
        }

        public void OnTriggerEnter(Collider other)
        {
            var root = other.gameObject.GetComponentInParent<IObjectRoot>();
            var hitInfo = new HitInfo {RootGameObject = root != null ? root.Root : other.gameObject};
            HitCallback?.Invoke(hitInfo);
            Destroy(gameObject);
        }
    }
}