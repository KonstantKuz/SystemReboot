using Combat.Hit;
using UnityEngine;

namespace Combat.Weapon.HitListener
{
    public class AddForceInfo : MonoBehaviour, IHitListener
    {
        [SerializeField] private Transform _direction;
        [SerializeField] private float _force;
    
        public void OnHit(ref HitInfo info)
        {
            var forceInfo = HitForceInfo.Create(_direction.forward * _force);
            info.AppendInfo(forceInfo);
        }
    }
}