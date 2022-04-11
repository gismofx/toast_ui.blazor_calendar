using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar.Services.JsonConverters
{
    internal class TUITemplateConverter : JsonConverter<TUITemplate>
    {
        public override TUITemplate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TUITemplate value, JsonSerializerOptions options)
        {
            //List<KeyValuePair<string, object>> list = value. as List<KeyValuePair<string, object>>;
            //writer.WriteStartArray();
            foreach (var item in value)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("functionName");
                writer.WriteStringValue(item.FunctionName);
                writer.WritePropertyName("args");
                writer.WriteStringValue(item.Args);
                writer.WritePropertyName("functionBody");
                writer.WriteStringValue(item.FunctionBody);
                writer.WriteEndObject();
            }
            //writer.WriteEndArray();
        }
    }
}
                /*class MyConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                List<KeyValuePair<string, object>> list = value as List<KeyValuePair<string, object>>;
                writer.WriteStartArray();
                foreach (var item in list)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName(item.Key);
                    writer.WriteValue(item.Value);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                // TODO...
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(List<KeyValuePair<string, object>>);
            }
        }*/


