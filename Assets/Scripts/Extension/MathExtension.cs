using UnityEngine;

namespace Extension
{
    public static class MathExtension
    {
        public static bool IsZero(this float value)
        {
            return Mathf.Abs(value) < Mathf.Epsilon;
        }
    }
}