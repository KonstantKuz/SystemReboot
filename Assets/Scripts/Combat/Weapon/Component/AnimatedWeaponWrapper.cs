using System;
using Animation;
using Combat.Hit;
using Combat.Weapon.Base;
using Extension;
using JetBrains.Annotations;
using UnityEngine;
using Util;
using AnimationEvent = Util.AnimationEvent;

namespace Combat.Weapon.Component
{
    public class AnimatedWeaponWrapper : IDisposable
    {
        private readonly BaseWeapon _baseWeapon;
        private readonly Animator _animator;
        private readonly AnimationEventHandler _animationEventHandler;

        private Action<HitInfo> _hitCallback;
        public AnimatedWeaponWrapper(BaseWeapon weapon, [CanBeNull] Animator animator)
        {
            _baseWeapon = weapon;
            _animator = animator;
            if (_animator == null) return;
            _animationEventHandler = _animator.gameObject.RequireComponent<AnimationEventHandler>();
            _animationEventHandler.OnAnimationEvent += OnAnimationEvent;
        }

        public BaseWeapon Weapon => _baseWeapon;

        public void Fire(Action<HitInfo> hitCallback)
        {
            if (_animator != null)
            {
                _hitCallback = hitCallback;
                _animator.Play(AnimatorHash.ATTACK_HASH);
            }
            else
            {
                _baseWeapon.Fire(hitCallback);
            }
        }

        private void OnAnimationEvent(string eventName)
        {
            if(eventName != AnimationEvent.FIRE) return;
            _baseWeapon.Fire(_hitCallback);
        }

        public void Dispose()
        {
            if(_animator == null) return;
            _animationEventHandler.OnAnimationEvent -= OnAnimationEvent;
        }
    }
}