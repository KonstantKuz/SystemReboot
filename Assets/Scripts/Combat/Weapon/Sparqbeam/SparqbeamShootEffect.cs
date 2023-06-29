using Combat.HitEffect;
using Combat.Weapon.Base;
using DigitalRuby.ThunderAndLightning;
using Extension;
using UnityEngine;

namespace Combat.Weapon.Sparqbeam
{
    public class SparqbeamShootEffect : WeaponShootEffect
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private LightningBoltPrefabScript _lightning;
        private IHitNotifier _hitNotifier;
        private IHitNotifier HitNotifier => _hitNotifier ??= gameObject.RequireComponent<IHitNotifier>();

        public override void Awake()
        {
            base.Awake();
            HitNotifier.OnHit += SetLightningHitPoint;
        }

        private void SetLightningHitPoint(HitInfo hitInfo)
        {
            if(hitInfo == null) return;
            _lightning.Destination.transform.position = hitInfo.RaycastHit.Value.point;
        }

        protected override void PlayEffect()
        {
            _lightning.transform.SetParent(null);
            _lightning.transform.position = _barrel.transform.position;
            _lightning.transform.rotation = _barrel.transform.rotation;
            _lightning.Trigger();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            HitNotifier.OnHit -= SetLightningHitPoint;
        }
    }
}