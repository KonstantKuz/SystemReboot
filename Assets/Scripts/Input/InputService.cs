using System;
using Extension;
using Zenject;

namespace Input
{
    public class InputService : MonoInstaller
    {
        private const string MOUSE_WHEEL = "Mouse ScrollWheel";
        public event Action OnLeftMouseClick;
        public event Action OnRightMouseClick;
        public event Action<int> OnMouseScroll;

        public override void InstallBindings()
        {
            Container.Bind<InputService>().FromInstance(this).AsSingle();
        }

        public void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnLeftMouseClick?.Invoke();
            }
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                OnRightMouseClick?.Invoke();
            }
            if (!UnityEngine.Input.GetAxis(MOUSE_WHEEL).IsZero())
            {
                OnMouseScroll?.Invoke(UnityEngine.Input.GetAxis(MOUSE_WHEEL) > 0 ? 1 : -1);
            }
        }
    }
}
