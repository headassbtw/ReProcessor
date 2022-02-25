using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Logging;
using SiraUtil.Zenject;
using ReProcessor.Configuration;
using ReProcessor.Installers;

namespace ReProcessor
{
    [Plugin(RuntimeOptions.DynamicInit), NoEnableDisable] // NoEnableDisable suppresses the warnings of not having a OnEnable/OnStart and OnDisable/OnExit methods
    public class Plugin
    {
        [Init]
        public void Init(Zenjector zenjector, Logger logger, Config config)
        {
            zenjector.UseLogger(logger);
            zenjector.UseMetadataBinder<Plugin>();

            zenjector.Install<AppInstaller>(Location.App, config.Generated<PluginConfig>());
            zenjector.Install<MenuInstaller>(Location.Menu);
        }
    }
}