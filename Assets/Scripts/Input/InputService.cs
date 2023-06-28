using System;
using Extension;
using Zenject;

namespace Input
{
    public class InputService : MonoInstaller
    {
        private const string MOUSE_WHEEL = "Mouse ScrollWheel";
        public event Action OnMouseClick;
        public event Action<int> OnMouseScroll;
        
        public override void InstallBindings()
        {
            Container.Bind<InputService>().FromInstance(this).AsSingle();
        }

        public void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnMouseClick?.Invoke();
            }
            if (!UnityEngine.Input.GetAxis(MOUSE_WHEEL).IsZero())
            {
                OnMouseScroll?.Invoke(UnityEngine.Input.GetAxis(MOUSE_WHEEL) > 0 ? 1 : -1);
            }
        }
    }
}
