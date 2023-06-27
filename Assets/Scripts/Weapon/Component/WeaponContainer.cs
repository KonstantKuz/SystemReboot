using UnityEngine;

namespace Weapon.Component
{
    public class WeaponContainer : MonoBehaviour
    {
        [SerializeField] private Transform _weponRoot;
        public Transform WeaponRoot => _weponRoot;
    }
}