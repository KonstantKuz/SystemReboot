using Extension;
using Weapon.Component;

namespace Player.Service
{
    public class PlayerService
    {
        public PlayerService(PlayerCharacterController playerCharacterController)
        {
            Player = playerCharacterController;
            WeaponContainer = playerCharacterController.RequireComponentInChildren<WeaponContainer>();
        }

        public PlayerCharacterController Player { get; }
        public WeaponContainer WeaponContainer { get; }
    }
}