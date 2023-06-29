using Extension;
using UnityEngine;

namespace Util
{
    public class SwitchRigOnAwake : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _targetMesh;
        [SerializeField] private Transform _targetRootBone;
        
        public void Awake()
        {
            _targetMesh.SwitchRig(_targetRootBone);
        }
    }
}
