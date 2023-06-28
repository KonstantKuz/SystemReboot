using BaseUnit;
using UnityEngine;
using Zenject;

namespace Player.Service
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Unit _player;

        public override void InstallBindings()
        {
            Container.Bind<PlayerService>().AsSingle().WithArguments(_player).NonLazy();
        }
    }
}