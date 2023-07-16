using Combat.Hit;
using UnityEngine;

namespace Player.HookController
{
    public struct HookInfo
    {
        public RaycastHit Hit;
        public Vector3 HookOrigin;
        public float HookForce;
    }

    public static class HookInfoExtension
    {
        public static HitInfo ToHitInfo(this HookInfo hookInfo)
        {
            var hitInfo = HitInfo.FromRaycastHit(hookInfo.Hit);
            var direction = (hookInfo.Hit.point - hookInfo.HookOrigin).normalized;
            hitInfo.AppendInfo(HitForceInfo.Create(direction * hookInfo.HookForce));
            hitInfo.AppendInfo(DamageInfo.Create(0));
            hitInfo.AppendInfo(hookInfo);
            return hitInfo;
        }
    }
}