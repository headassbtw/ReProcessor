using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using ReProcessor.Configuration;

namespace ReProcessor.Files
{
    public class IO
    {
        public static Preset LoadJson(string path)
        {
            
            StreamReader r = new StreamReader(path);
            string json = r.ReadToEnd();
            r.Close();
            Preset items = JsonConvert.DeserializeObject<Preset>(json);
            List<CameraSetting> toReplaceBloom = new List<CameraSetting>();
            if (items == null)
            {
                throw new InvalidExpressionException();
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