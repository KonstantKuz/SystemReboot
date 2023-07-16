using Extension;
using SuperMaxim.Core.Extensions;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Ragdoll.Sliceable
{
    public class SliceableRigHandler : MonoBehaviour
    {
        [SerializeField] private RigBuilder _rigBuilder;
        [SerializeField] private GameObject[] _rigs;
        
        public ISliceable _sliceable;

        private void Awake()
        {
            _sliceable = gameObject.RequireComponent<ISliceable>();
            _sliceable.OnBeforeSlice += HandleSlice;
        }

        private void HandleSlice()
        {
            if (_rigBuilder == null) return;
            _rigBuilder.enabled = false;
            Destroy(_rigBuilder);
            _rigs.ForEach(it =>
            {
                it.SetActive(false);
                Destroy(it);
            });
        }

        private void OnDestroy()
        {
            _sliceable.OnBeforeSlice -= HandleSlice;
        }
    }
}