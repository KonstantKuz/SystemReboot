using UnityEngine;

namespace Unit.Target
{
    public class UnitTarget : MonoBehaviour, ITarget
    {
        [SerializeField] private Transform _center;
        public Transform Center => _center;
    }
}