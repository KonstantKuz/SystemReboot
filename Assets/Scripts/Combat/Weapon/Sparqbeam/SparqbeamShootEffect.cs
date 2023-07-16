using Combat.Hit;
using Combat.Weapon.Base;
using Combat.Weapon.HitListener;
using DigitalRuby.ThunderAndLightning;
using UnityEngine;

namespace Combat.Weapon.Sparqbeam
{
    public class SparqbeamShootEffect : WeaponShootEffect, IHitListener
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private LightningBoltPrefabScript _lightning;

        public void OnHit(ref HitInfo hitInfo)
        {
            if(hitInfo.RaycastHit == null) return;
            var distance = (hitInfo.RaycastHit.Value.point - _barrel.position).magnitude;
            _lightning.Destination.transform.position = _barrel.position + _barrel.forward * distance;
        }

        protected override void PlayEffect()
        {
            _lightning.transform.SetParent(null);
            _lightning.transform.position = _barrel.position;
            _lightning.transform.rotation = _barrel.rotation;
            _lightning.Trigger();
        }
    }
}