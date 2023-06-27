using Zenject;

namespace Level.Service
{
    public static class LevelServicesInstaller
    {
        public static void InstallBindings(DiContainer container)
        {
            container.Bind<LevelService>().AsSingle().NonLazy();
        }
    }
}