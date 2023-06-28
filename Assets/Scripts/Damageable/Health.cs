using System;
using UnityEngine;

namespace Damageable
{
    public class Health : MonoBehaviour, IDamageable
    {
        public event Action<DamageInfo> OnDeath;
        
        public void TakeDamage(DamageInfo damageInfo)
        {
            OnDeath?.Invoke(damageInfo);
        }
    }
}
