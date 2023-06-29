using Combat.Damageable;
using Common;
using Extension;
using Messenger;
using Messenger.Message;
using Unit.Target;
using UnityEngine;

namespace Unit
{
    public class BaseUnit : MonoBehaviour, IObjectRoot
    {
        private GameObjectMessenger _messenger;
        private bool _isActive;
        public GameObject Root => gameObject;
        public ITarget SelfTarget { get; private set; }
        public Health Health { get; private set; }

        public bool IsActive
        {
            get => _isActive;
            private set
            {
                _isActive = value;
                _messenger.Publish(new UnitActiveStateChangedMessage {IsActive = _isActive});
            }
        }

        public void Awake()
        {
            _messenger = new GameObjectMessenger(gameObject);
            SelfTarget = gameObject.RequireComponent<ITarget>();
            Health = gameObject.RequireComponent<Health>();
            IsActive = true;
            Health.OnDeath += OnDeath;
        }

        private void OnDeath(DamageInfo damageInfo)
        {
            IsActive = false;
            _messenger.Publish(new UnitDeadMessage());
        }
    }
}