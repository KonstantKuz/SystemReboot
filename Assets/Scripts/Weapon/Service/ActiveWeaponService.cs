namespace Weapon.Service
{
    public class ActiveWeaponService
    {
        private int _weaponId = 0;

        public string ActiveWeapon => WeaponFactory.WEAPONS[_weaponId];
        
        public void SwitchWeapon(int offset)
        {
            _weaponId += offset;
            if (_weaponId < 0) _weaponId = WeaponFactory.WEAPONS.Length - 1;
            if (_weaponId > WeaponFactory.WEAPONS.Length - 1) _weaponId = 0;
        }
    }
}