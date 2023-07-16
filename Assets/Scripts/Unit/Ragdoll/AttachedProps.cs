using SuperMaxim.Core.Extensions;
using UnityEngine;

namespace Unit.Ragdoll
{
    public class AttachedProps : MonoBehaviour
    {
        [SerializeField] private Transform[] _props;

        public void DetachAll() => _props.ForEach(Detach);

        private void Detach(Transform prop)
        {
            prop.SetParent(null);
            if (TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.isKinematic = false;
            }
        }
    }
}