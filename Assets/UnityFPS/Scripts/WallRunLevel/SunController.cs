using UnityEngine;

namespace UnityFPS.Scripts.WallRunLevel
{
    public class SunController : MonoBehaviour
    {
        public float rotationSpeed = 20;

        void Update()
        {
            transform.Rotate(0 , rotationSpeed * Time.deltaTime, 0);

        }
    }
}
