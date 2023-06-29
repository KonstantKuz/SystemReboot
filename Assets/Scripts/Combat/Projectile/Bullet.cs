using System;
using Combat.Weapon.Base;
using UnityEngine;

namespace Combat.Projectile
{
    public class Bullet : BaseProjectile
    {
        [SerializeField] private float _speed;
        private bool _launched;

        public override void Launch(Action<HitInfo> hitCallback)
        {
            base.Launch(hitCallback);
            _launched = true;
        }

        private void Update()
        {
            if(!_launched) return;
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }
}