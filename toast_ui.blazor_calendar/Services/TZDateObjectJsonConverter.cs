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
    class TZDateObjectJsonConverter : JsonConverter<TUITzDate>
    {
        //e.g. "2021-04-15T18:20:11.584Z"
        private const string TZDateFormat = "yyyy-MM-ddTHH:mm:ss.FFFZzz";

        public override TUITzDate Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<TUITzDate>(ref reader);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TUITzDate tzDateValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(tzDateValue._date.ToString());
        
    }
    /*
     * writer.WriteStringValue(dateTimeValue.ToString(
                    TZDateFormat, CultureInfo.InvariantCulture));
    */
}
