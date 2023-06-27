using Zenject;

namespace Session.Service
{
    public class SessionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SessionService>().AsSingle().NonLazy();
        }
    }
}