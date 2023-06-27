using System;
using UnityEngine;

namespace Animation
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action<string> OnAnimationEvent;

        public void AnimationCallback(string eventName)
        {
            OnAnimationEvent?.Invoke(eventName);
        }
    }
}
