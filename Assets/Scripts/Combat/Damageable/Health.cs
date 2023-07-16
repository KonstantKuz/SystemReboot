using System;
using System.Linq;
using Combat.Hit;
using Combat.Weapon.HitListener;
using UnityEngine;

namespace Combat.Damageable
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private MonoBehaviour[] _hitListeners;
        
        private float _currentValue;

        public event Action<HitInfo> OnDamageTaken;
        public event Action<HitInfo> OnDeath;

        public void Init(float value)
        {
            _currentValue = value;
        }
        
        public void TakeDamage(HitInfo hitInfo)
        {
            if(!hitInfo.TryGetAdditionalInfo(out DamageInfo damageInfo)) return;
            _currentValue -= damageInfo.Damage;
            var isCriticalInfo = IsCriticalInfo.Create(_currentValue < damageInfo.Damage);
            hitInfo.AppendInfo(isCriticalInfo);
            hitInfo.NotifyListeners(_hitListeners.Select(it => it as IHitListener));
            OnDamageTaken?.Invoke(hitInfo);
            if (_currentValue <= 0)
            {
                OnDeath?.Invoke(hitInfo);
            }
        }
    }
}
