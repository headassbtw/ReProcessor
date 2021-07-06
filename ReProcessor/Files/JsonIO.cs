using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPA.Utilities;
using Newtonsoft.Json;
using static ReProcessor.Config;
using ReProcessor.Files;

namespace ReProcessor
{
    static class PresetExtensions
    {
        public static string PRESET_SAVE_PATH = Path.Combine(UnityGame.UserDataPath, "ReProcessor", "Presets");
        public static void Save(this Preset preset)
        {
            if (!Directory.Exists(PRESET_SAVE_PATH))
                Directory.CreateDirectory(PRESET_SAVE_PATH);

            JsonIO.SaveJson(preset, Path.Combine(PRESET_SAVE_PATH, preset.Name) + ".json");
        }
        public static void Load(this Preset preset)
        {
            if (!Directory.Exists(PRESET_SAVE_PATH))
                Directory.CreateDirectory(PRESET_SAVE_PATH);
            if (!File.Exists(Path.Combine(PRESET_SAVE_PATH, preset.Name) + ".json"))
            {
                File.Create(Path.Combine(PRESET_SAVE_PATH, preset.Name) + ".json").Close();
                Preset temp = new Preset(preset.Name);
                temp.Save();
            }
            try
            {
                JsonIO.LoadJson(Path.Combine(PRESET_SAVE_PATH, preset.Name) + ".json");
            }
            catch (JsonSerializationException)
            {
                Plugin.RedoConfigFile("invalid");
            }
        }
        public static Preset Load(string presetName)
        {
            if (!Directory.Exists(PRESET_SAVE_PATH))
                Directory.CreateDirectory(PRESET_SAVE_PATH);
            if (!File.Exists(Path.Combine(PRESET_SAVE_PATH, presetName) + ".json"))
                JsonIO.NewPresetFile(presetName);
            try
            {
                File.Create(Path.Combine(PRESET_SAVE_PATH, presetName) + ".json").Close();
                Preset temp = new Preset(presetName);
                temp.Save();
            }
            catch
            {
                //this is a thing now?? tf goin on
            }

            return JsonIO.LoadJson(Path.Combine(PRESET_SAVE_PATH, presetName) + ".json");

            
            
        }
    }

    class JsonIO
    {
        public static string PRESET_SAVE_PATH = Path.Combine(UnityGame.UserDataPath, "ReProcessor", "Presets");
        public static void NewPresetFile(string name)
        {
            File.Create(Path.Combine(PRESET_SAVE_PATH, name) + ".json").Close();
            using (StreamWriter w = new StreamWriter(Path.Combine(PRESET_SAVE_PATH, name) + ".json"))
            {
                var newPreset = new Preset(name);
                JsonSerializer serializer = new JsonSerializer();
                string contents = JsonConvert.SerializeObject(newPreset);
                var jtw = new JsonTextWriter(w);

                jtw.Formatting = Formatting.Indented;
                serializer.Serialize(jtw, newPreset);
                w.Close();
            }
        }
        public static Preset LoadJson(string path)
        {
            StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();
            r.Close();
            Preset items = JsonConvert.DeserializeObject<Preset>(json);
            List<CameraSetting> toReplaceBloom = new List<CameraSetting>();
            if (items == null)
            {
                Plugin.RedoConfigFile("invalid");
                return LoadJson(path);
            }
            return items;
        }
        public static void SaveJson(Preset preset, string path)
        {
            using (StreamWriter w = new StreamWriter(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                string contents = JsonConvert.SerializeObject(preset);
                var jtw = new JsonTextWriter(w);

                jtw.Formatting = Formatting.Indented;
                serializer.Serialize(jtw, preset);
                w.Close();
            }
        }
    }
}
