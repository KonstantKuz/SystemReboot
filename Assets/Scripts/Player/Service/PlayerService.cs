using BaseUnit;
using Extension;
using Player.Component;
using Weapon.Component;

namespace Player.Service
{
    public class PlayerService
    {
        public PlayerService(Unit player)
        {
            Player = player;
            PlayerAttack = Player.gameObject.RequireComponent<PlayerAttack>();
        }

        public Unit Player { get; }
        public PlayerAttack PlayerAttack { get; }

        public void SwitchWeapon(BaseWeapon weapon) => PlayerAttack.SwitchWeapon(weapon);
    }
}