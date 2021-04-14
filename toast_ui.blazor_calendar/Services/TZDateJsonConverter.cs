using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Services
{
    class TZDateJsonConverter : JsonConverter<DateTimeOffset>
    {
        //e.g. "2021-04-15T18:20:11.584Z"
        private const string TZDateFormat = @"yyyy-MM-ddTHH:mm:ss.fffZ";

        public override DateTimeOffset Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            DateTimeOffset.ParseExact(reader.GetString(),
             TZDateFormat, CultureInfo.InvariantCulture);

        public override void Write(
            Utf8JsonWriter writer,
            DateTimeOffset dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue.ToString());
        
    }
}
