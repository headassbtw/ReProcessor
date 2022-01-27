using ReProcessor.Configuration;
using ReProcessor.Managers;
using Zenject;

namespace ReProcessor.Installers
{
    internal class AppInstaller : Installer
    {
        private readonly PluginConfig _config;

        public AppInstaller(PluginConfig config)
        {
            _config = config;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_config).AsSingle();
            
            Container.BindInterfacesAndSelfTo<ConfigManager>().AsSingle();
            
        }
    }
}