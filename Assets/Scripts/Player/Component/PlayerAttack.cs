using UnityEngine;
using Weapon.Component;

namespace Player.Component
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerWeaponWrapper _weaponWrapper;

        public void SetActiveWeapon(BaseWeapon weapon)
        {
            _weaponWrapper = new PlayerWeaponWrapper(weapon);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _weaponWrapper.Fire(null);
            }
        }
    }
}