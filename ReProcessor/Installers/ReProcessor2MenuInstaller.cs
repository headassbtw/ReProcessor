using ReProcessor.Managers;
using ReProcessor.UI;
using ReProcessor.UI.Views.ColorBoostView;
using ReProcessor.UI.Views.ConfigManager;
using ReProcessor.UI.Views.NoBloomError;
using Zenject;

namespace ReProcessor.Installers
{
    internal class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<CamManager>().AsSingle();
            Container.Bind<LastResort>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesAndSelfTo<ButtonManager>().AsSingle();
            Container.Bind<NoBloomController>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<ColorBoostController>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<ConfigViewController>().FromNewComponentAsViewController().AsSingle();
            Container.Bind<UI.SettingsFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}