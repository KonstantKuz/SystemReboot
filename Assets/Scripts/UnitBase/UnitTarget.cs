using UnityEngine;

namespace UnitBase
{
    public class UnitTarget : MonoBehaviour, ITarget
    {
        [SerializeField] private Transform _center;
        public Transform Center => _center;
    }
}