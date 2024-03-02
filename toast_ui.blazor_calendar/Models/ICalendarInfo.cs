using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Models
{
    // TODO: Can we use Color instead of string?
    // (Would be better for the user and is more logial)    
    public interface ICalendarInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("color")]
        public Color? Color { get; set; }

        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("backgroundColor")]
        public Color? BackgroundColor { get; set; }

        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("dragBackgroundColor")]
        public Color? DragBackgroundColor { get; set; }

        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("borderColor")]
        public Color? BorderColor { get; set; }
    }
}
