using System;
using Zenject;
using SiraUtil;
using ReProcessor.UI;

namespace ReProcessor.Installers
{
    internal class MenuSettingsInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ButtonManager>().AsSingle();
            Container.Bind<BloomSettingsView>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<IInitializable>().To<rSettingsFlowCoordinator>().FromNewComponentOnNewGameObject(nameof(rSettingsFlowCoordinator)).AsSingle();
        }
    }
}
