using System.IO;
using Extension;
using Player.Component;
using Player.Service;
using UnityEngine;
using Util;
using Weapon.Component;

namespace Weapon.Service
{
    public class WeaponFactory
    {
        private const string RAPIER_ID = "Hands_Rapier";
        private const string SPARQBEAM_ID = "Hands_Sparqbeam";

        private readonly PlayerService _playerService;
        public WeaponFactory(PlayerService playerService)
        {
            _playerService = playerService;
        }
        
        public void CreatePlayerWeapon()
        {
            var prefabPath = Path.Combine(ResourcesPath.WEAPON_PREFABS, SPARQBEAM_ID);
            var prefab = Resources.Load(prefabPath);
            var weaponGameObject = Object.Instantiate(prefab, _playerService.WeaponContainer.WeaponRoot) as GameObject;
            var weapon = weaponGameObject.RequireComponent<BaseWeapon>();
            var playerAttack = _playerService.Player.gameObject.RequireComponent<PlayerAttack>();
            playerAttack.SetActiveWeapon(weapon);
        }
    }
}