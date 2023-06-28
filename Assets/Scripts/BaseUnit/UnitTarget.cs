using UnityEngine;

namespace BaseUnit
{
    public class UnitTarget : MonoBehaviour, ITarget
    {
        [SerializeField] private Transform _center;
        public Transform Center => _center;
    }
}