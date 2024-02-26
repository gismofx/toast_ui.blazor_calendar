using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Theme
{
    public interface ICommonTheme
    {
        /// <summary>
        /// Background color of calendar
        /// Default Value: white
        /// </summary>
        [JsonPropertyName("backgroundColor")]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Border of calendar
        /// Default Value: 1px solid #e5e5e5
        /// </summary>
        [JsonPropertyName("border")]
        public string Border { get; set; }

        [JsonPropertyName("gridSelection")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object GridSelection { get; set; }
        //gridSelection: {
        //  backgroundColor: string;
        //  border: string;
        //};

        /// <summary>
        /// Day of the week
        /// Default Value: { color: '#333' }	
        /// </summary>
        [JsonPropertyName("dayName")]
        public IColorProperty DayName { get; set; }

        [JsonPropertyName("holiday")]
        public IColorProperty Holiday { get; set; }

        [JsonPropertyName("saturday")]
        public IColorProperty Saturday { get; set; }

        [JsonPropertyName("today")]
        public IColorProperty Today { get; set; }

    }
}
