using System;
using UnityEngine;

namespace Ragdoll.Sliceable
{
    public interface ISliceable
    {
        event Action OnBeforeSlice;
        void Slice(SliceParams sliceParams);
    }

    public struct SliceParams
    {
        public Plane Plane;
        public Vector3 Force;
    }
}