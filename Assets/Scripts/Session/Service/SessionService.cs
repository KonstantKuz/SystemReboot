using Player.Service;
using Weapon.Service;

namespace Session.Service
{
    public class SessionService
    {
        public readonly WeaponFactory _weaponFactory;
        public readonly PlayerService _playerService;
        public SessionService(WeaponFactory weaponFactory, PlayerService playerService)
        {
            _weaponFactory = weaponFactory;
            _playerService = playerService;
            Init();
        }

        private void Init()
        {
            var initialWeapon = _weaponFactory.CreatePlayerWeapon();
            _playerService.SwitchWeapon(initialWeapon);
        }
    }
}