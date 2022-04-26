using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Timezone options defined here https://nhn.github.io/tui.calendar/latest/Timezone
    /// </summary>
    [JsonConverter(typeof(TUICalendarTimeZoneOptionJsonConverter))]
    public class TUICalendarTimeZoneOption
    {
        /// <summary>
        /// List of TUITimeZone calendar time zones
        /// </summary>
        public List<TimeZoneInfo> zones { get; set; }

        /// <summary>
        /// Function Expression to return the Display Label
        /// </summary>
        [JsonIgnore]
        public Func<TimeZoneInfo, string> DisplayLabelFunction { get; set; }

        /// <summary>
        /// Function Expression to return the Tool Tip
        /// </summary>
        [JsonIgnore]
        public Func<TimeZoneInfo, string> ToolTipFunction { get; set; }

        /// <summary>
        /// Constructs a default TUICalendarTimeZoneOption
        /// </summary>
        public TUICalendarTimeZoneOption()
        {
            zones = new List<TimeZoneInfo>();
        }

        /// <summary>
        /// Constructs TUICalendarTimeZoneOption with expressions to set the display label and tool tip
        /// </summary>
        /// <param name="displayLabelFunction">Function Expression to return the Display Label</param>
        /// <param name="toolTipFunction">Function Expression to return the Tool Tip</param>
        public TUICalendarTimeZoneOption(Func<TimeZoneInfo, string> displayLabelFunction, Func<TimeZoneInfo, string> toolTipFunction)
        {
            zones = new List<TimeZoneInfo>();
            DisplayLabelFunction = displayLabelFunction;
            ToolTipFunction = toolTipFunction;
        }
    }
}
