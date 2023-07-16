using UnityEngine;

namespace Ragdoll.Sliceable
{
    public struct SliceInfo
    {
        public Plane Plane;
        public Vector3 Force;

        public static SliceInfo Create(Transform planeOrigin, float force)
        {
            return new SliceInfo
            {
                Plane = new Plane(planeOrigin.up, planeOrigin.position),
                Force = planeOrigin.forward * force,
            };
        }
    }
}