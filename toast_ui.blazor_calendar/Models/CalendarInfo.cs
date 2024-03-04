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
    public class CalendarInfo
    {
        /// <summary>
        /// The calendar id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The calendar name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The text color when schedule is displayed
        /// </summary>
        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("color")]
        public Color? Color { get; set; }

        /// <summary>
        /// The background color schedule is displayed
        /// </summary>
        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("backgroundColor")]
        public Color? BackgroundColor { get; set; }

        /// <summary>
        /// The color of left border or bullet point when schedule is displayed
        /// </summary>
        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("dragBackgroundColor")]
        public Color? DragBackgroundColor { get; set; }

        /// <summary>
        /// The background color when schedule dragging
        /// </summary>
        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("borderColor")]
        public Color? BorderColor { get; set; }
    }
}
