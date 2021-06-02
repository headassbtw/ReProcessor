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
            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType().Equals(typeof(PyramidBloomRendererSO.Pass))){
                var c = (PyramidBloomRendererSO.Pass)value;
                var objValue = c.ToString();
                serializer.Serialize(writer, objValue);
            }
            else
            {
                var objValue = value.ToString();
                serializer.Serialize(writer, objValue);
            }
        }
    }
}
