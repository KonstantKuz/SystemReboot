using System.IO;
using Extension;
using UnityEngine;
using Util;
using Weapon.Component;

namespace Weapon.Service
{
    public class WeaponFactory
    {
        private const string RAPIER_ID = "Weapon_Rapier";
        private const string SPARQBEAM_ID = "Weapon_Sparqbeam";
        
        public BaseWeapon CreatePlayerWeapon()
        {
            var prefabPath = Path.Combine(ResourcesPath.WEAPON_PREFABS, RAPIER_ID);
            var prefab = Resources.Load(prefabPath);
            var instance = Object.Instantiate(prefab) as GameObject;
            return instance.RequireComponent<BaseWeapon>();
        }
    }
}