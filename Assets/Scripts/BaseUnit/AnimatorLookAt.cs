using Extension;
using JetBrains.Annotations;
using UnityEngine;

namespace BaseUnit
{
    public class AnimatorLookAt : MonoBehaviour
    {
        [SerializeField] private float _weight;
        private Animator _animator;
        
        [CanBeNull]
        public Transform Target { get; set; }

        private void Awake()
        {
            _animator = gameObject.RequireComponent<Animator>();
        }

        public void OnAnimatorIK(int layerIndex)
        {
            if(Target == null) return;
            
            _animator.SetLookAtWeight(_weight);
            _animator.SetLookAtPosition(Target.position);
        }
    }
}
