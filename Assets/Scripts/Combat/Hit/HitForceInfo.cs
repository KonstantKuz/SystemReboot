using UnityEngine;

namespace Combat.Hit
{
    public struct HitForceInfo
    {
        public Vector3 Force { get; private set; }
        public static HitForceInfo Create(Vector3 force) => new HitForceInfo {Force = force};
    }
}