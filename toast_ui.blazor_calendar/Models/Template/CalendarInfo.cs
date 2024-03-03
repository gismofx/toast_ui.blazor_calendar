using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using toast_ui.blazor_calendar.Services.JsonConverters;

#pragma warning disable CS8632 // The annotation for nullable reference types may only be used in code within a #nullable annotation context.

namespace toast_ui.blazor_calendar.Models.Template
{
    public class CalendarInfo : ICalendarInfo
    {

        // TODO: Translation for specific properties if possible
        public CalendarInfo()
        {

        }

        private string _Id;
        [JsonPropertyName("id")]
        public string Id { get => _Id; set => _Id = value; }

        private string _Name;
        [JsonPropertyName("name")]
        public string Name { get => _Name; set => _Name = value; }

        private Color? _Color;
        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("color")]
        public Color? Color { get => _Color; set => _Color = value; }

        private Color? _BackgroundColor;
        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("backgroundColor")]
        public Color? BackgroundColor { get => _BackgroundColor; set => _BackgroundColor = value; }

        private Color? _DragBackgroundColor;
        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("dragBackgroundColor")]
        public Color? DragBackgroundColor { get => _DragBackgroundColor; set => _DragBackgroundColor = value; }

        private Color? _BorderColor;
        [JsonConverter(typeof(ColorJsonConverter))]
        [JsonPropertyName("borderColor")]
        public Color? BorderColor { get => _BorderColor; set => _BorderColor = value; }
    }


    /*

id: string;
name: string;
color?: string;
backgroundColor?: string;
dragBackgroundColor?: string;
borderColor?: string;


     */
}
