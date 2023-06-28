using Common;
using Extension;
using UnityEngine;
using Damageable;
using Messenger;
using Messenger.Message;

namespace UnitBase
{
    public class Unit : MonoBehaviour, IObjectRoot
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
        }
    }
}