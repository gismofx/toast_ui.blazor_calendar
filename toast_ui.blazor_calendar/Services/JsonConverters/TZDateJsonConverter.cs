using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            DateTimeOffset? value = null;
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    // A simple DateTimeOffset string "value"
                    return DateTimeOffset.ParseExact(reader.GetString(), TZDateFormat, CultureInfo.InvariantCulture);
                case JsonTokenType.StartObject:
                    {
                        while(reader.Read())
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



        /*
* // A DateTimeOffset string embedded in an object { "d" : "value" }
               DateTimeOffset? value = null;
               while (reader.Read())
               {
                   switch (reader.TokenType)
                   {
                       case JsonTokenType.EndObject:
                          //reader.Skip();
                          break;
                       case JsonTokenType.PropertyName:
                           var match = reader.ValueTextEquals(_date);

                           if (match)
                           { 
                               reader.Read();
                               while (reader.TokenType != JsonTokenType.PropertyName) { reader.Read(); }
                               reader.Read();
                               var propValue = reader.GetString();
                               value = DateTimeOffset.ParseExact(propValue, TZDateFormat, CultureInfo.InvariantCulture);
                           }
                           else
                               reader.Skip();
                           break;
                       default:
                           throw new JsonException();
                   }
                   return value.GetValueOrDefault();
               }
*/
        public override void Write(
            Utf8JsonWriter writer,
            DateTimeOffset dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue);// JsonSerializer.Serialize(dateTimeValue));

        private DateTimeOffset? ReadDateProperty(ref Utf8JsonReader reader)
        {
            
            //var x = reader.GetString();
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
            //var token = reader.TokenType;
            //return null;
        }

        private int ReadTzOffset(ref Utf8JsonReader reader)
        {
            var token = reader.TokenType;
            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                //return reader.GetInt32();
                return 0;
                //throw new JsonException();
            }

            return reader.GetInt32();

            //Console.WriteLine(reader.GetString());
            //reader.Read();
            //Console.WriteLine(reader.GetString());
            //if (token == JsonTokenType.Number)
            //    return reader.GetInt32();
            throw new JsonException();
            //return 0;
        }


    }
}
