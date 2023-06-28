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
        public static string[] WEAPONS = {RAPIER_ID, SPARQBEAM_ID};
        
        public BaseWeapon CreateWeapon(string weaponId)
        {
            var prefabPath = Path.Combine(ResourcesPath.WEAPON_PREFABS, weaponId);
            var prefab = Resources.Load(prefabPath);
            var instance = Object.Instantiate(prefab) as GameObject;
            return instance.RequireComponent<BaseWeapon>();
        }
    }
}