using Weapon.Service;

namespace Session.Service
{
    public class SessionService
    {
        public readonly WeaponFactory _weaponFactory;
        public SessionService(WeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            Init();
        }

        private void Init()
        {
            _weaponFactory.CreatePlayerWeapon();
        }
    }
}