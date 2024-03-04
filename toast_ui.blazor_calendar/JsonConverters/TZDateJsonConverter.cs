using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.JsonConverters
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
            DateTimeOffset? value = null;
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    // A simple DateTimeOffset string "value"
                    return DateTimeOffset.ParseExact(reader.GetString(), TZDateFormat, CultureInfo.InvariantCulture);
                case JsonTokenType.StartObject:
                    {
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndObject)
                            {
                                return value.GetValueOrDefault();
                            }
                            if (reader.TokenType == JsonTokenType.PropertyName)
                            {
                                var propName = reader.GetString();
                                switch (propName.ToUpper())
                                {
                                    case "TZOFFSET":
                                        var offset = ReadTzOffset(ref reader);
                                        break;
                                    case "D":
                                        value = ReadDateProperty(ref reader);
                                        break;
                                    default:
                                        throw new JsonException($"Cannot parse {propName}");
                                }
                            }
                        }
                    }
                    break;
            }
            throw new JsonException("End Object Not found for TZDate Parser");
        }




        public override void Write(
            Utf8JsonWriter writer,
            DateTimeOffset dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue);// JsonSerializer.Serialize(dateTimeValue));

        /// <summary>
        /// Read the nested date property
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>DateTimeOffset</returns>
        /// <exception cref="JsonException"></exception>
        private DateTimeOffset? ReadDateProperty(ref Utf8JsonReader reader)
        {
            DateTimeOffset? parsedDt = null;
            reader.Read();//Read into the "d" property
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Date Property must start with a start object");
            }
            reader.Read();

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException("Sub property of date 'd' expected");
            }

            if (!reader.ValueTextEquals("d"))
            {
                throw new JsonException($"Sub property of date 'd' expected name is d. Value is {reader.GetString()}");
            }

            reader.Read();
            if (reader.TokenType == JsonTokenType.String)
            {
                var propValue = reader.GetString();
                parsedDt = DateTimeOffset.ParseExact(propValue, TZDateFormat, CultureInfo.InvariantCulture);
                reader.Read();
            }

            if (reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException($"End Object expected for 'd' property. Received: {reader.TokenType}");
            }
            return parsedDt;
        }

        /// <summary>
        /// Read the Tz Offset. It's the current time zone offset in minutes
        /// Not useful since returned date is UTC
        /// We parse anyway
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        private int ReadTzOffset(ref Utf8JsonReader reader)
        {
            var token = reader.TokenType;
            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                return 0;
            }
            if (reader.TryGetInt32(out var offset))
            {
                return offset;
            }
            throw new JsonException();
        }


    }
}
