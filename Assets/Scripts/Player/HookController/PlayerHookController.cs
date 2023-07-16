using System.Collections;
using Extension;
using Input;
using UnityEngine;
using UnityFPS.Scripts;
using Zenject;

namespace Player.HookController
{
    public class PlayerHookController : MonoBehaviour
    {
        [SerializeField] private float _verticalOffset = 1f;
        [SerializeField] private float _precisionDistance = 1f;
        [SerializeField] private float _force;
        
        [Inject] private InputService _inputService;
        
        private PlayerCharacterController _characterController;
        private Hook _hook;
        
        private void Awake()
        {
            _characterController = gameObject.RequireComponent<PlayerCharacterController>();
            _hook = gameObject.RequireComponentInChildren<Hook>();
            _inputService.OnRightMouseClick += Hook;
        }

        private void Hook()
        {
            _hook.FireHook(_force, OnHookCallback);
        }

        private void OnHookCallback(HookInfo hookInfo)
        {
            StartCoroutine(PullUp(hookInfo));
        }

        private IEnumerator PullUp(HookInfo hookInfo)
        {
            _characterController.Jump();
            while (Vector3.Distance(_characterController.transform.position, hookInfo.Hit.point) > _precisionDistance)
            {
                yield return null;
                var direction = hookInfo.Hit.point - (_characterController.transform.position + Vector3.up * _verticalOffset);
                _characterController.characterVelocity = (hookInfo.HookForce * direction.normalized);
            }
        }

        private void OnDestroy()
        {
            _inputService.OnRightMouseClick -= Hook;
        }
    }
}