using Combat.Weapon.Base;
using Combat.Weapon.Component;
using Common;
using Extension;
using Messenger;
using Messenger.Message;
using Player.Service;
using Unit.Movement;
using Unit.Target;
using UnityEngine;
using Util;
using Zenject;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour, IMessageListener<UnitActiveStateChangedMessage>
    {
        [SerializeField] private LookAtWrapper _lookAtWrapper;
        [SerializeField] private float _attackDistance;
        [SerializeField] private int _attackInterval;

        private AnimatedWeaponWrapper _weaponWrapper;
        private Timer _attackTimer;
        
        [Inject] private PlayerService _playerService;

        private ITarget Target => _playerService.Player.SelfTarget;
        private bool IsTargetInAttackRange =>
            Vector3.Distance(transform.position, Target.Center.position) < _attackDistance;
        
        private void Awake()
        {
            var weapon = gameObject.RequireComponentInChildren<BaseWeapon>();
            var animator = gameObject.GetComponentInChildren<Animator>();
            _weaponWrapper = new AnimatedWeaponWrapper(weapon, animator);
            _attackTimer = Timer.IntervalTimer(_attackInterval, Fire);
        }

        private void Update()
        {
            if (IsTargetInAttackRange)
            {
                _lookAtWrapper.LookAt(Target.Center.position);
            }
            else
            {
                _lookAtWrapper.Reset();
            }
        }

        private void Fire()
        {
            if (IsTargetInAttackRange) return;
            _weaponWrapper.Fire(null);
        }

        public void OnMessage(UnitActiveStateChangedMessage message)
        {
            if(message.IsActive) return;
            _attackTimer.Dispose();
            enabled = false;
        }
    }
}