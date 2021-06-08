using IPA;
using IPA.Config.Stores;
using IPA.Loader;
using ReProcessor.Installers;
using ReProcessor.Files;
using static ReProcessor.Config;
using SiraUtil;
using SiraUtil.Attributes;
using SiraUtil.Zenject;
using static ReProcessor.PresetExtensions;
using Conf = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;
using System;
using System.IO;

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
            PresetName = "preset";
            Log = logger;
            Config = conf.Generated<Config>();
            preset = Load(PresetName);
            try
            {
                var a = preset.Bloom.Count;
            }
            catch (NullReferenceException)
            {
                RedoConfigFile("empty");
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
            zenjector.OnGame<GameplayInstaller>().ShortCircuitForMultiplayer();

            

            // Specify the scene name or contract or installer!
            //zenjector.On("Menu").Register<Installers.GameplayInstaller>();
        }

        internal static void RedoConfigFile(string reason = "")
        {
            if (reason != "")
                Log.Critical($"Preset file was {reason}! Rebuilding...");
            if(reason == "")
                Log.Critical("Rebuilding preset file...");
            File.Delete(Path.Combine(PRESET_SAVE_PATH, $"{PresetName}.json"));
            preset = new Preset(PresetName);
            preset.Save();
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
