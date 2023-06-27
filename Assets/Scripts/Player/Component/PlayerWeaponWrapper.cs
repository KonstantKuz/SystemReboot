using System;
using Animation;
using Extension;
using ModestTree;
using UnityEngine;
using Util;
using Weapon.Component;
using AnimationEvent = Util.AnimationEvent;

namespace Player.Component
{
    public class PlayerWeaponWrapper : IDisposable
    {
        private readonly BaseWeapon _baseWeapon;
        private readonly Animator _animator;
        private readonly AnimationEventHandler _animationEventHandler;

        private Action<GameObject> _hitCallback;
        public PlayerWeaponWrapper(BaseWeapon weapon)
        {
            _baseWeapon = weapon;
            _animator = _baseWeapon.gameObject.GetComponentInChildren<Animator>();
            if (_animator == null) return;
            _animationEventHandler = _animator.gameObject.RequireComponent<AnimationEventHandler>();
            _animationEventHandler.OnAnimationEvent += OnAnimationEvent;
        }

        public void Fire(Action<GameObject> hitCallback)
        {
            if (_animator != null)
            {
                _hitCallback = hitCallback;
                _animator.SetTrigger(AnimatorHash.ATTACK_HASH);
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