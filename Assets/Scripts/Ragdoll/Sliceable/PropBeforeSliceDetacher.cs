using Extension;
using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Ragdoll.Sliceable
{
    public class PropBeforeSliceDetacher : MonoBehaviour
    {
        [SerializeField] private Transform[] _detach;

        private ISliceable _sliceable;

        private void Awake()
        {
            _sliceable = gameObject.RequireComponent<ISliceable>();
            _sliceable.OnBeforeSlice += OnBeforeSlice;
        }

        private void OnBeforeSlice()
        {
            _detach.ForEach(it =>
            {
                it.transform.SetParent(null);
                if (it.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.isKinematic = false;
                }
            });
        }

        private void OnDestroy()
        {
            _sliceable.OnBeforeSlice -= OnBeforeSlice;
        }
    }
}