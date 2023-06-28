using Zenject;

namespace Weapon.Service
{
    public class WeaponServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WeaponFactory>().AsSingle();
            Container.Bind<ActiveWeaponService>().AsSingle();
        }
    }
}