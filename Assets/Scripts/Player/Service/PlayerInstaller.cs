using UnityEngine;
using Zenject;

namespace Player.Service
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCharacterController _player;

        public override void InstallBindings()
        {
            Container.Bind<PlayerService>().AsSingle().WithArguments(_player).NonLazy();
        }
    }
}