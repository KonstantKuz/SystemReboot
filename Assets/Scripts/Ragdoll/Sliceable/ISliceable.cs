using System;

namespace Ragdoll.Sliceable
{
    public interface ISliceable
    {
        event Action OnBeforeSlice;
        void Slice(SliceInfo sliceInfo);
    }
}