using System;
using Combat.Damageable;
using UnityEngine;

namespace Player.HookController
{
    public class Hook : MonoBehaviour
    {
        public void FireHook(float force, Action<HookInfo> hookCallback)
        {
            if(!Physics.Raycast(transform.position, transform.forward, out var hit)) return;
            var hookInfo = new HookInfo
            {
                HookForce = force,
                HookOrigin = transform.position,
                Hit = hit,
            };
            var damageable = hit.collider.gameObject.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(hookInfo.ToHitInfo());
            }
            else
            {
                hookCallback.Invoke(hookInfo);
            }
        }
    }
}