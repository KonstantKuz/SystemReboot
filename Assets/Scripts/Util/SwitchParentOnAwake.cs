using Extension;
using UnityEngine;

namespace Util
{
    public class SwitchParentOnAwake : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _destination;

        private void Awake()
        {
            _target.SetParent(_destination);
            _target.ResetLocalTransform();
        }
    }
}
