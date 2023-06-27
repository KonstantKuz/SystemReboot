using UnityEngine;

namespace Ragdoll.Sliceable
{
    public interface ISliceable
    {
        void Slice(SliceParams sliceParams);
    }

    public struct SliceParams
    {
        public Plane Plane;
        public Vector3 Force;
    }
}