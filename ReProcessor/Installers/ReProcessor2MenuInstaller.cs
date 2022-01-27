using ReProcessor.Configuration;
using ReProcessor.Managers;
using ReProcessor.UI;
using ReProcessor.UI.Views.NoBloomError;
using ReProcessor.UI.Views.TestView;
using Zenject;

namespace ReProcessor.Installers
{
    internal class MenuInstaller : Installer
    {
        public MenuInstaller()
        {
            
        }
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CamManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<LastResort>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesAndSelfTo<ButtonManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<NoBloomController>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesAndSelfTo<ColorBoostController>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesAndSelfTo<ConfigViewController>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<rSettingsFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}