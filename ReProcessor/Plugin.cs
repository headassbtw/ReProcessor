using IPA;
using IPA.Config.Stores;
using IPA.Loader;
using ReProcessor.Installers;
//using ReProcessor.Files;
using static ReProcessor.Config;
using SiraUtil;
using SiraUtil.Attributes;
using SiraUtil.Zenject;
using static ReProcessor.PresetExtensions;
using Conf = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;
using System;

namespace ReProcessor
{
    [Plugin(RuntimeOptions.DynamicInit), Slog]
    public class Plugin
    {
        //internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Preset preset { get; set; }
        internal static Config Config { get; set; }
        internal static string PresetName { get; private set; }

        [Init]
        public Plugin(Conf conf, Zenjector zenjector, IPALogger logger, PluginMetadata metadata)
        {
            PresetName = "test";
            Log = logger;
            Config = conf.Generated<Config>();
            try
            {
                preset = Load(PresetName);
            }
            catch (Exception)
            {
                var p = new Preset(PresetName, new BloomConfig(), new ColorBoostConfig());
                p.Save();
                preset = Load(PresetName);
            }
            zenjector.On<PCAppInit>().Pseudo(Container =>
            {
                Container.BindLoggerAsSiraLogger(logger);
                //Container.BindInstance(Config).AsSingle();
                Container.BindInstance(new UBinder<Plugin, PluginMetadata>(metadata));
            });
            
            //Instance = this;
            
            //zenjector.OnApp<MyMainInstaller>().WithParameters(10); // Use Zenject's installer parameter system!
            zenjector.OnMenu<MenuSettingsInstaller>();
            zenjector.OnMenu<MenuInstaller>();
            zenjector.OnGame<GameplayInstaller>();

            

            // Specify the scene name or contract or installer!
            //zenjector.On("Menu").Register<Installers.GameplayInstaller>();
        }

        internal void CreateFolders()
        {
            
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            //new GameObject("ReProcessorController").AddComponent<ReProcessorController>();

        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");

        }
    }
}
