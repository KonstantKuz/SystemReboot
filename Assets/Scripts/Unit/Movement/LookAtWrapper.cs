using Extension;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Unit.Movement
{
    public class LookAtWrapper : MonoBehaviour
    {
        [SerializeField] private Transform _pointer;
        [SerializeField] private Rig _aimRig;
        [SerializeField] private float _rotationSpeed;

        public void LookAt(Vector3 target)
        {
            var lookDirection = (target - transform.position).XZ();
            var lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            _pointer.position = target;
            _aimRig.weight = Mathf.Lerp(_aimRig.weight, 1f, Time.deltaTime * 5f);
        }

        public void Reset()
        {
            _aimRig.weight = 0f;
        }
    }
}
