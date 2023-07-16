using UnityEngine;

namespace Unit.Model
{
    [CreateAssetMenu(menuName = "Model/Unit", fileName = "UnitModel")]
    public class UnitModel : ScriptableObject
    {
        public float Health;
        public float AttackInterval;
    }
}