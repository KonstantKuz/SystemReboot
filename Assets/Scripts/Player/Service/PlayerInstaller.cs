﻿using Unit;
using UnityEngine;
using Zenject;

namespace Player.Service
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private BaseUnit _player;

        public override void InstallBindings()
        {
            Container.Bind<PlayerService>().AsSingle().NonLazy();
            Container.Resolve<PlayerService>().Init(_player);
        }
    }
}