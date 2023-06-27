using Level.Service;
using Zenject;

namespace App
{
    public class MainSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            LevelServicesInstaller.InstallBindings(Container);
        }
    }
}