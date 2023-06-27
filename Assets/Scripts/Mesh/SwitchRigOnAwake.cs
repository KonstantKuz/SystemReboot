using UnityEngine;

namespace Mesh
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
