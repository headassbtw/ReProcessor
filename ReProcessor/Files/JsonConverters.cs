using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReProcessor.Extensions;

namespace ReProcessor.Files
{
    class PassJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value.GetType().Equals(typeof(System.String)))
            {
                PyramidBloomRendererSO.Pass outEnum;
                Enum.TryParse<PyramidBloomRendererSO.Pass>(reader.Value.ToString(), out outEnum);
                //Plugin.Log.Notice($"committing value \"{reader.Value}\" to config, as an enum");
                return outEnum;
            }
            else if (reader.Value.GetType().Equals(typeof(System.Single)) || reader.Value.GetType().Equals(typeof(System.Double)))
            {
                //Plugin.Log.Notice($"committing value \"{reader.Value}\" to config, as a float");
                return (System.Single)float.Parse(reader.Value.ToString());
            }
            else if (reader.Value.GetType().EqualsInt())
            {
                //Plugin.Log.Notice($"committing value \"{reader.Value}\" to config, as an int");
                return (System.Int32)Int32.Parse(reader.Value.ToString());
            }
            else
            {
                //Plugin.Log.Notice($"committing value \"{reader.Value}\" to config, as no other valid type was found, although the reader thought it was a {reader.Value.GetType()}");
                return reader.Value.ToString();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType().Equals(typeof(System.Single)) || value.GetType().Equals(typeof(System.Single)))
            {
                var objValue = float.Parse(value.ToString());
                serializer.Serialize(writer, objValue);
            }
            else if (value.GetType().Equals(typeof(System.Int32)))
            {
                var objValue = Int32.Parse(value.ToString());
                serializer.Serialize(writer, objValue);
            }
            else
            {
                Enum.TryParse<PyramidBloomRendererSO.Pass>(value.ToString(), out var value2);
                var objValue = Extensions.ToString(value2);
                serializer.Serialize(writer, objValue);
            }
        }
    }
    
}
