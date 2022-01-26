using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IPA.Utilities;
using ReProcessor.Configuration;
using SiraUtil.Logging;
using UnityEngine;
using Zenject;

namespace ReProcessor.Managers
{
    public class ConfigManager : IInitializable, IDisposable
    {
        public Preset CurrentPreset;
        public Preset TempPreset;
        public static string PRESET_SAVE_PATH = Path.Combine(UnityGame.UserDataPath, "ReProcessor", "Presets");
        private SiraLog _log;
        ConfigManager(SiraLog log)
        {
            _log = log;
        }

        
        public Dictionary<string, Preset> Presets = new Dictionary<string, Preset>();


        public void Set(string name)
        {
            if (!Presets.ContainsKey(name))
            {
                _log.Notice($"Preset {name} does not exist, or is not loaded");
                return;
            }

            CurrentPreset = Presets[name];
            TempPreset = CurrentPreset;
        }
        public void GetPresets()
        {
            Presets.Clear();
            var presets = Directory.GetFiles(PRESET_SAVE_PATH);

            foreach (var preset in presets)
            {
                try
                {
                    Preset pr = Files.IO.LoadJson(preset);
                    Presets.Add(pr.Name, pr);
                }
                catch (ArgumentException e)
                {
                    _log.Error("Preset already exists. Did you forget to name them differently?");
                }
            }
        }
        
        public void Initialize()
        {
            Presets = new Dictionary<string, Preset>();
            if (!Directory.Exists(PRESET_SAVE_PATH))
            {
                _log.Error("Presets folder does not exist, creating...");
                Directory.CreateDirectory(PRESET_SAVE_PATH);
            }

            int presets = Directory.GetFiles(PRESET_SAVE_PATH).Length;
            _log.Notice($"{presets} Presets.");
            if (presets <= 0)
            {
                _log.Error("No presets were found, creating...");
                Files.IO.SaveJson(new Preset(), Path.Combine(PRESET_SAVE_PATH, "Default.json"));
            }
            GetPresets();
            Set(Presets.Keys.FirstOrDefault()); //TODO: FIX SHIT
        }

        public void Save(string name)
        {
            CurrentPreset = TempPreset;
            Presets[name] = CurrentPreset;
            Files.IO.SaveJson(Presets[name], Path.Combine(PRESET_SAVE_PATH, $"{name}.json"));
        }

        public void Dispose()
        {
            
        }
    }
}