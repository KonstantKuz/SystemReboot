using UnityEngine;

namespace Combat.Weapon.Model
{
    [CreateAssetMenu(menuName = "Model/Weapon", fileName = "WeaponModel")]
    public class WeaponModel : ScriptableObject
    {
        public float Damage;
        public float Distance;
    }
}