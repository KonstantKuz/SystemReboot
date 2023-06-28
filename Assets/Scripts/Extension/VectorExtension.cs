using UnityEngine;

namespace Extension
{
    public static class VectorExtension
    {
        public static bool IsInsideCone(this Vector3 target, 
            Vector3 coneOrigin,
            Vector3 coneDirection,
            float coneAngle,
            float minDistance,
            float maxDistance)
        {
            return IsInsideCone(target, coneOrigin, coneDirection, coneAngle / 2) &&
                   IsInsideDistanceRange(target, coneOrigin, minDistance, maxDistance);
        }
        
        private static bool IsInsideCone(Vector3 target,
            Vector3 coneOrigin,
            Vector3 coneDirection, 
            float maxAngle)
        {
            var targetDirection = target - coneOrigin;
            var angle = Vector3.Angle(coneDirection, targetDirection);
            return angle <= maxAngle;
        }

        private static bool IsInsideDistanceRange(Vector3 target,
            Vector3 origin, 
            float distanceMin, 
            float distanceMax)
        {
            var distance = Vector3.Distance(origin, target);
            return distance > distanceMin && distance < distanceMax;
        }
        
        public static float DistanceXZ(Vector3 a, Vector3 b) => Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));

        public static Vector3 XZ(this Vector3 v) => new Vector3(v.x, 0, v.z);
        
        public static Vector2 ToVector2XZ(this Vector3 v) => new Vector2(v.x, v.z);
        
        public static Vector2 WorldToScreenPoint(this Vector3 v) => UnityEngine.Camera.main.WorldToScreenPoint(v);
    }
}