using Extension;
using UnityEngine;

namespace Combat.Weapon.Base
{
    public abstract class WeaponShootEffect : MonoBehaviour
    {
        private IFireNotifier _notifier;
        private IFireNotifier Notifier => _notifier ??= gameObject.RequireComponent<IFireNotifier>();

        public virtual void Awake()
        {
            Notifier.OnFire += PlayEffect;
        }

        protected abstract void PlayEffect();

        public virtual void OnDestroy()
        {
            Notifier.OnFire -= PlayEffect;
        }
    }
}
