using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Services.JsonConverters
{
    class TZDateJsonConverter : JsonConverter<DateTimeOffset>
    {
        //{
        //  "tzOffset": null,
        //  "d": {
        //      "d": "2024-02-25T05:00:00.000Z"
        //      }
        //}

        //e.g. "2021-04-15T18:20:11.584Z"
        private const string TZDateFormat = @"yyyy-MM-ddTHH:mm:ss.fffZ";

        static byte[] _date = Encoding.UTF8.GetBytes("d"); //"_date"

        public override DateTimeOffset Read(ref Utf8JsonReader reader, 
                                            Type typeToConvert, 
                                            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    // A simple DateTimeOffset string "value"
                    return DateTimeOffset.ParseExact(reader.GetString(), TZDateFormat, CultureInfo.InvariantCulture);
                case JsonTokenType.StartObject:
                    {
                        // A DateTimeOffset string embedded in an object { "_date" : "value" }
                        DateTimeOffset? value = null;
                        while (reader.Read())
                        {
                            switch (reader.TokenType)
                            {
                                case JsonTokenType.EndObject:
                                    return value.GetValueOrDefault();
                                case JsonTokenType.PropertyName:
                                    var match = reader.ValueTextEquals(_date);

                                    if (match)
                                    { 
                                        reader.Read();
                                        while (reader.TokenType != JsonTokenType.PropertyName) { reader.Read(); }
                                        reader.Read();
                                        var propValue = reader.GetString();
                                        value = DateTimeOffset.ParseExact(propValue, TZDateFormat, CultureInfo.InvariantCulture);
                                        return value.Value;
                                    }
                                    else
                                        reader.Skip();
                                    break;
                                default:
                                    throw new JsonException();
                            }
                        }
                    }
                    break;
            }
            throw new JsonException();
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTimeOffset dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue);// JsonSerializer.Serialize(dateTimeValue));
        
    }
}
