using Damageable;
using Extension;
using Input;
using UnityEngine;
using Weapon.Component;
using Zenject;

namespace Player.Component
{
    public class PlayerAttack : MonoBehaviour
    {
        [Inject] private InputService _inputService;
        
        private WeaponContainer _weaponContainer;
        private AnimatedWeaponWrapper _weaponWrapper;

        private WeaponContainer WeaponContainer =>
            _weaponContainer ??= gameObject.RequireComponentInChildren<WeaponContainer>();

        private void Awake()
        {
            _inputService.OnMouseClick += Attack;
        }

        public void ReplaceWeapon(BaseWeapon weapon)
        {
            weapon.transform.SetParent(WeaponContainer.WeaponRoot, false);
            DestroyActiveWeapon();
            _weaponWrapper?.Dispose();
            _weaponWrapper = new AnimatedWeaponWrapper(weapon,  weapon.gameObject.GetComponentInChildren<Animator>());
        }

        private void DestroyActiveWeapon()
        {
            if(_weaponWrapper == null || _weaponWrapper.Weapon == null) return;
            Destroy(_weaponWrapper.Weapon.gameObject);
        }

        private void Attack()
        {
            if (_weaponWrapper == null) return;
            _weaponWrapper.Fire(OnHit);
        }

        public void OnHit(HitInfo hitInfo)
        {
            if(hitInfo == null) return;
            if (hitInfo.TryGetRootDamageable(out var damageable))
            {
                damageable.TakeDamage(DamageInfo.Create(int.MaxValue, hitInfo));
            }
            
            var hitColliderInfo = hitInfo.RaycastHit == null ? string.Empty : $"Hit collider name {hitInfo.RaycastHit.Value.collider.name}";
            Debug.Log($"Hit! Info : RootGameObject name {hitInfo.RootGameObject.name}. {hitColliderInfo}");
        }

        public void OnDestroy()
        {
            _inputService.OnMouseClick -= Attack;
            _weaponWrapper.Dispose();
        }
    }
}