using System;
using Combat.Hit;
using Combat.Projectile;
using Combat.Weapon.Base;
using UnityEngine;

namespace Combat.Weapon.Component
{
    public class ProjectileWeapon : BaseWeapon
    {
        [SerializeField] private Transform _barrel;
        [SerializeField] private BaseProjectile _projectile;
        
        public override void Fire(Action<HitInfo> hitCallback)
        {
            CreateProjectile().Launch(hitCallback);
        }

        public virtual BaseProjectile CreateProjectile()
        {
            var projectile = Instantiate(_projectile);
            projectile.transform.position = _barrel.position;
            projectile.transform.rotation = _barrel.rotation;
            return projectile;
        }
    }
}
