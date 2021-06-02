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
            PyramidBloomRendererSO.Pass outEnum;
            Enum.TryParse<PyramidBloomRendererSO.Pass>(reader.Value.ToString(), out outEnum);
            return outEnum;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType().Equals(typeof(System.Single)))
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
                Enum.TryParse<PyramidBloomRendererSO.Pass>((string)value, out var value2);
                var objValue = Extensions.ToString(value2);
                serializer.Serialize(writer, objValue);
            }
        }
    }
    class EnumJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return valueType.String;
            else
            {

                valueType outEnum;
                Enum.TryParse<valueType>((string)reader.Value, out outEnum);
                return outEnum;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var c = (valueType)value;
            var objValue = c.ToString();
            serializer.Serialize(writer, objValue);
        }
    }
}
