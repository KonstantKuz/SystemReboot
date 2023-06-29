using Combat.Weapon.Service;
using Extension;
using Input;
using Player.Component;
using Unit;

namespace Player.Service
{
    public class PlayerService
    {
        private readonly WeaponFactory _weaponFactory;
        private readonly InputService _inputService;
        private readonly ActiveWeaponService _activeWeaponService;
        
        public PlayerService(WeaponFactory weaponFactory, InputService inputService, ActiveWeaponService activeWeaponService)
        {
            _weaponFactory = weaponFactory;
            _inputService = inputService;
            _activeWeaponService = activeWeaponService;
        }

        public BaseUnit Player { get; private set; }
        public PlayerAttack PlayerAttack { get; private set; }

        public void Init(BaseUnit player)
        {
            Player = player;
            PlayerAttack = Player.gameObject.RequireComponent<PlayerAttack>();
            ReplaceWeapon();
            _inputService.OnMouseScroll += SwitchWeapon;
        }

        private void SwitchWeapon(int offset)
        {
            _activeWeaponService.SwitchWeapon(offset);
            ReplaceWeapon();
        }

        private void ReplaceWeapon()
        {
            var weapon = _weaponFactory.CreateWeapon(_activeWeaponService.ActiveWeapon);
            PlayerAttack.ReplaceWeapon(weapon);
        }
    }
}