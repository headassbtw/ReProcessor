using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPA.Utilities;
using Newtonsoft.Json;
//using ReProcessor.Files;
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
        public static Preset Load(string presetName)
        {
            if (!Directory.Exists(PRESET_SAVE_PATH))
                Directory.CreateDirectory(PRESET_SAVE_PATH);
            return JsonIO.LoadJson(Path.Combine(PRESET_SAVE_PATH, presetName) + ".json");
        }
    }

    class JsonIO
    {
        public static Preset LoadJson(string path)
        {
            StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();
            Preset items = JsonConvert.DeserializeObject<Preset>(json);
            r.Close();
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
