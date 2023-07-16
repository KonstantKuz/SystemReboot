using Combat.Damageable;
using Combat.Hit;
using Combat.Weapon.Base;
using Combat.Weapon.Component;
using Extension;
using Input;
using UnityEngine;
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
            _inputService.OnLeftMouseClick += Attack;
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

        private void OnHit(HitInfo hitInfo)
        {
            if(hitInfo.IsEmpty()) return;
            
            if (hitInfo.TryGetRootDamageable(out var damageable))
            {
                hitInfo.AppendInfo(DamageInfo.Create(int.MaxValue));
                damageable.TakeDamage(hitInfo);
            }
            
            var hitColliderInfo = hitInfo.RaycastHit == null ? string.Empty : $"Hit collider name {hitInfo.RaycastHit.Value.collider.name}";
            Debug.Log($"Hit! Info : RootGameObject name {hitInfo.RootGameObject.name}. {hitColliderInfo}");
        }

        public void OnDestroy()
        {
            _inputService.OnLeftMouseClick -= Attack;
            _weaponWrapper.Dispose();
        }
    }
}