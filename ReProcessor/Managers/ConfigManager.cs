using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IPA.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ReProcessor.Configuration;
using SiraUtil.Logging;
using Zenject;

namespace ReProcessor.Managers
{
    internal class ConfigManager : IInitializable
    {
        private static readonly string PresetSavePath = Path.Combine(UnityGame.UserDataPath, nameof(ReProcessor), "Presets");

        private readonly SiraLog _log;
        private readonly JsonSerializer _jsonSerializer;

        public ConfigManager(SiraLog log)
        {
            _log = log;
            _jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new StringEnumConverter() },
                Formatting = Formatting.Indented
            });
        }

        public Dictionary<string, Preset> Presets { get; private set; } = new();
        public Preset CurrentPreset { get; private set; }
        public Preset TempPreset { get; private set; }

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
            var presets = Directory.GetFiles(PresetSavePath);

            foreach (var preset in presets)
            {
                try
                {
                    Preset pr = LoadJson(preset);
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
            if (!Directory.Exists(PresetSavePath))
            {
                _log.Error("Presets folder does not exist, creating...");
                Directory.CreateDirectory(PresetSavePath);
            }

            int presets = Directory.GetFiles(PresetSavePath).Length;
            _log.Notice($"{presets} Presets.");
            if (presets <= 0)
            {
                _log.Error("No presets were found, creating...");
                SaveJson(Preset.CreateDefault(), Path.Combine(PresetSavePath, "Default.json"));
            }

            GetPresets();
            Set(Presets.Keys.FirstOrDefault()); //TODO: FIX SHIT
        }

        public void Save(string name)
        {
            CurrentPreset = TempPreset;
            Presets[name] = CurrentPreset;
            SaveJson(Presets[name], Path.Combine(PresetSavePath, $"{name}.json"));
        }

        private Preset LoadJson(string path)
        {
            using var streamReader = new StreamReader(path);
            using var jsonTextReader = new JsonTextReader(streamReader);
            return _jsonSerializer.Deserialize<Preset>(jsonTextReader) ?? Preset.CreateDefault(); // TODO
        }

        private void SaveJson(Preset preset, string path)
        {
            using var streamWriter = new StreamWriter(path);
            using var jsonTextWriter = new JsonTextWriter(streamWriter);
            _jsonSerializer.Serialize(jsonTextWriter, preset);
        }
    }
}