using System.Linq;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using ReProcessor.Configuration;
using ReProcessor.Installers;
using ReProcessor.Managers;
using SiraUtil.Logging;
using IPALogger = IPA.Logging.Logger;

namespace ReProcessor
{
    [Plugin(RuntimeOptions.DynamicInit),
     NoEnableDisable] // NoEnableDisable supresses the warnings of not having a OnEnable/OnStart
    // and OnDisable/OnExit methods
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal PluginConfig _config;

        [Init]
        public void Init(Zenjector zenjector, IPALogger logger, Config config)
        {
            Instance = this;
            Log = logger;

            zenjector.UseLogger(logger);
            zenjector.UseMetadataBinder<Plugin>();

            
            zenjector.Install<AppInstaller>(Location.App, config.Generated<PluginConfig>());
            zenjector.Install<MenuInstaller>(Location.Menu);
        }
    }
}