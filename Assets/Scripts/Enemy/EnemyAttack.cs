using BaseUnit;
using Extension;
using Player.Service;
using UnityEngine;
using Util;
using Weapon.Component;
using Zenject;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private AnimatorLookAt _animatorLookAt;
        [SerializeField] private float _attackDistance;
        [SerializeField] private int _attackInterval;
        [SerializeField] private float _rotationSpeed;

        private AnimatedWeaponWrapper _weaponWrapper;
        private Timer _attackTimer;
        
        [Inject] private PlayerService _playerService;

        private void Awake()
        {
            var weapon = gameObject.RequireComponentInChildren<BaseWeapon>();
            var animator = gameObject.GetComponentInChildren<Animator>();
            _weaponWrapper = new AnimatedWeaponWrapper(weapon, animator);
            _attackTimer = Timer.IntervalTimer(_attackInterval, Fire);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _playerService.Player.transform.position) < _attackDistance)
            {
                _animatorLookAt.Target = _playerService.Player.SelfTarget.Center;
            }
            if(_animatorLookAt.Target == null) return;
            LookAt(_animatorLookAt.Target.position);
        }

        public void LookAt(Vector3 target)
        {
            var lookDirection = (target - transform.position).XZ();
            var lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        private void Fire()
        {
            if(_animatorLookAt.Target == null) return;
            _weaponWrapper.Fire(null);
        }
    }
}