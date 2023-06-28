using System;
using Extension;
using UnityEngine;
using Weapon.Component;

namespace Player.Component
{
    public class PlayerAttack : MonoBehaviour
    {
        private WeaponContainer _weaponContainer;
        private AnimatedWeaponWrapper _weaponWrapper;

        private WeaponContainer WeaponContainer =>
            _weaponContainer ??= gameObject.RequireComponentInChildren<WeaponContainer>();

        public void SwitchWeapon(BaseWeapon weapon)
        {
            weapon.transform.SetParent(WeaponContainer.WeaponRoot, false);
            DestroyActiveWeapon();
            _weaponWrapper?.Dispose();
            _weaponWrapper = new AnimatedWeaponWrapper(weapon,  weapon.gameObject.GetComponentInChildren<Animator>());
        }

        public void DestroyActiveWeapon()
        {
            if(_weaponWrapper == null || _weaponWrapper.Weapon == null) return;
            Destroy(_weaponWrapper.Weapon.gameObject);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _weaponWrapper.Fire(DebugHit);
            }
        }

        public void DebugHit(HitInfo hitInfo)
        {
            if(hitInfo == null) return;
            var hitColliderInfo = hitInfo.RaycastHit == null ? string.Empty : $"Hit collider name {hitInfo.RaycastHit.Value.collider.name}";
            Debug.Log($"Hit! Info : RootGameObject name {hitInfo.RootGameObject.name}. {hitColliderInfo}");
        }

        public void OnDestroy()
        {
            _weaponWrapper.Dispose();
        }
    }
}