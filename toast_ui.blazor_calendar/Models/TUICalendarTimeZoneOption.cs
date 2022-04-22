using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Timezone options defined here https://nhn.github.io/tui.calendar/latest/Timezone
    /// </summary>
    public class TUICalendarTimeZoneOption
    {
        /// <summary>
        /// List of TUITimeZone calendar time zones
        /// </summary>
        public List<TUITimeZone> zones { get; set; }

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
            zones = new List<TUITimeZone>();
        }

        /// <summary>
        /// Constructs TUICalendarTimeZoneOption with expressions to set the display label and tool tip
        /// </summary>
        /// <param name="displayLabelFunction">Function Expression to return the Display Label</param>
        /// <param name="toolTipFunction">Function Expression to return the Tool Tip</param>
        public TUICalendarTimeZoneOption(Func<TimeZoneInfo, string> displayLabelFunction, Func<TimeZoneInfo, string> toolTipFunction)
        {
            zones = new List<TUITimeZone>();
            DisplayLabelFunction = displayLabelFunction;
            ToolTipFunction = toolTipFunction;
        }

        /// <summary>
        /// Converts a TimeZoneInfo into a TUI Defined Time Zone object
        /// </summary>
        /// <param name="timeZoneInfo">TimeZoneInfo</param>
        /// <returns>TUITimeZone</returns>
        public TUITimeZone ToTuiTimeZone(TimeZoneInfo timeZoneInfo)
        {
            //if unable to get teh IanaId return a null object
            if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(timeZoneInfo.Id, out string timezoneName)) return null;

            //create default options based on the example https://nhn.github.io/tui.calendar/latest/Timezone
            string positiveOrNegative = timeZoneInfo.BaseUtcOffset < TimeSpan.Zero ? "-" : "+";
            string displayLabel = $"GMT{positiveOrNegative}{timeZoneInfo.BaseUtcOffset:hh\\:mm}";
            string tooltip = timeZoneInfo.StandardName;

            //if the user specified a function to get the
            //display label use that
            if (DisplayLabelFunction != null)
            {
                displayLabel = DisplayLabelFunction(timeZoneInfo);
            }

            //if the user specified a function to get the
            //tool tip use that
            if (ToolTipFunction != null)
            {
                tooltip = ToolTipFunction(timeZoneInfo);
            }

            return new TUITimeZone
            {
                timezoneName = timezoneName,
                displayLabel = displayLabel,
                tooltip = tooltip
            };
        }
    }
}
