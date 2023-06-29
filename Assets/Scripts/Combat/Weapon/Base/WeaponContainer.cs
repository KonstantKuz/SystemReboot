using UnityEngine;

namespace Combat.Weapon.Base
{
    public class WeaponContainer : MonoBehaviour
    {
        [SerializeField] private Transform _weponRoot;
        public Transform WeaponRoot => _weponRoot;
    }
}