using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Defines the available Views for the Calendar
    /// </summary>
    public enum TUIViews
    {
        /// <summary>
        /// Month View
        /// </summary>
        [JsonPropertyName("month")]
        Month,
        /// <summary>
        /// Week View
        /// </summary>
        [JsonPropertyName("week")]
        Week,
        /// <summary>
        /// Day View
        /// </summary>
        [JsonPropertyName("day")]
        Day
    }
}
