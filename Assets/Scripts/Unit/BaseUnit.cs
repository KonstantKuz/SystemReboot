using Combat.Damageable;
using Combat.Hit;
using Common;
using Extension;
using Messenger;
using Messenger.Message;
using SuperMaxim.Core.Extensions;
using Unit.Model;
using Unit.Target;
using UnityEngine;

namespace Unit
{
    public class BaseUnit : MonoBehaviour, IObjectRoot
    {
        [field:SerializeField] public UnitModel Model { get; private set; }
        
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
            Init();
        }

        public void Init()
        {
            IsActive = true;
            Health.Init(Model.Health);
            Health.OnDeath += OnDeath;
            GetComponentsInChildren<IInitializable<UnitModel>>().ForEach(it => it.Init(Model));
        }

        private void OnDeath(HitInfo hitInfo) => Kill();

        public void Kill() => IsActive = false;

        private void OnDestroy() => Health.OnDeath -= OnDeath;
    }
}