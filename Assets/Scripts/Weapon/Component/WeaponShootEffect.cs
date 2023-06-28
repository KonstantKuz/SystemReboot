using Extension;
using UnityEngine;

namespace Weapon.Component
{
    public abstract class WeaponShootEffect : MonoBehaviour
    {
        private IFireNotifier _notifier;
        private IFireNotifier Notifier => _notifier ??= gameObject.RequireComponent<IFireNotifier>();

        public virtual void Awake()
        {
            Notifier.OnShoot += PlayEffect;
        }

        protected abstract void PlayEffect();

        public virtual void OnDestroy()
        {
            Notifier.OnShoot -= PlayEffect;
        }
    }
}
