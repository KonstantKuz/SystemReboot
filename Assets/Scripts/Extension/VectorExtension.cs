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
    }
}