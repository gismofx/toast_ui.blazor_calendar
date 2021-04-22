using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Models;

namespace toast_ui.blazor_calendar.Services
{
    class TUICalendarViewNameJsonConverter : JsonConverter<TUICalendarViewName>
    {

        public override TUICalendarViewName Read(ref Utf8JsonReader reader,
                                            Type typeToConvert,
                                            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    // A simple DateTimeOffset string "value"
                    var viewName = reader.GetString();
                    switch (viewName)
                    {
                        case "month":
                            return TUICalendarViewName.Month;
                        case "week":
                            return TUICalendarViewName.Week;
                        case "day":
                            return TUICalendarViewName.Day;
                        default:
                            throw new JsonException();
                    }
                default:
                    throw new JsonException();
            }
        }

        public override void Write(
            Utf8JsonWriter writer,
            TUICalendarViewName viewName,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(viewName.Value);
        
    }
}
