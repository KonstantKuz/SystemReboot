using Extension;
using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Util
{
    public class CopyCollidersFromTo : MonoBehaviour
    {
        [SerializeField] private Transform _originRoot;
        [SerializeField] private Transform _destinationRoot;

        public void CopyColliders()
        {
            _originRoot.GetComponentsInChildren<Collider>().ForEach(originCollider =>
            {
                foreach (var destinationTransform in _destinationRoot.GetComponentsInChildren<Transform>())
                {
                    if (destinationTransform.name != originCollider.name) continue;
                    destinationTransform.gameObject.AddComponent(originCollider.GetType()).CopyPropertiesFrom(originCollider);
                }
            });
        }

        public void RemoveDestinationColliders()
        {
            _destinationRoot.GetComponentsInChildren<Collider>().ForEach(DestroyImmediate);
        }
    }
}