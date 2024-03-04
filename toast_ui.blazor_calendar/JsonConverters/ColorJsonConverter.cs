using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.JsonConverters
{
    /// <summary>
    /// Creates a JsonConverter for the Color class.
    /// </summary>
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var hexValue = reader.GetString();

            // Remove the hash sign, if present
            if (hexValue != null && hexValue.StartsWith("#"))
                hexValue = hexValue.Substring(1);

            return ColorTranslator.FromHtml("#" + hexValue);
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            var hexValue = ColorTranslator.ToHtml(value);
            writer.WriteStringValue(hexValue);
        }
    }
}
