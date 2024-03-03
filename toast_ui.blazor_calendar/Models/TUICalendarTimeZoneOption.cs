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
        public List<TUITimeZone> Zones { get; set; }

        /// <summary>
        /// Function Expression to return the Display Label
        /// </summary>
        [JsonIgnore]
        public Func<TimeZoneInfo, string> DisplayLabelFunction { get; set; } = null;

        /// <summary>
        /// Function Expression to return the Tool Tip
        /// </summary>
        [JsonIgnore]
        public Func<TimeZoneInfo, string> ToolTipFunction { get; set; } = null;

        /// <summary>
        /// Constructs a default TUICalendarTimeZoneOption
        /// </summary>
        public TUICalendarTimeZoneOption()
        {
            Zones = new List<TUITimeZone>();
        }

        /// <summary>
        /// Constructs TUICalendarTimeZoneOption with expressions to set the display label and tool tip
        /// </summary>
        /// <param name="displayLabelFunction">Function Expression to return the Display Label</param>
        /// <param name="toolTipFunction">Function Expression to return the Tool Tip</param>
        public TUICalendarTimeZoneOption(Func<TimeZoneInfo, string> displayLabelFunction, Func<TimeZoneInfo, string> toolTipFunction): this()
        {
            DisplayLabelFunction = displayLabelFunction;
            ToolTipFunction = toolTipFunction;
        }

        /// <summary>
        /// Convert a TimeZoneInfo object into a TUITimeZone Object
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        public bool AddTimeZone(TimeZoneInfo timeZoneInfo)
        {
            var TUItz = ToTuiTimeZone(timeZoneInfo);
            if (TUItz is null)
                return false;
            
            Zones.Add(TUItz);
            return true;
        }

        /// <summary>
        /// Converts a TimeZoneInfo into a TUI Defined Time Zone object
        /// </summary>
        /// <param name="timeZoneInfo">TimeZoneInfo</param>
        /// <returns>TUITimeZone</returns>
        private TUITimeZone ToTuiTimeZone(TimeZoneInfo timeZoneInfo)
        {
            //if unable to get the IanaId return a null object
            if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(timeZoneInfo.Id, out string timezoneName)) return null;

            //create default options based on the example https://nhn.github.io/tui.calendar/latest/Timezone
            //if the user specified a function to get the
            //display label use that
            string displayLabel;
            if (DisplayLabelFunction != null)
            {
                displayLabel = DisplayLabelFunction(timeZoneInfo);
            }
            else
            {
                string positiveOrNegative = timeZoneInfo.BaseUtcOffset < TimeSpan.Zero ? "-" : "+";
                displayLabel = $"GMT{positiveOrNegative}{timeZoneInfo.BaseUtcOffset:hh\\:mm}";
            }

            /* Why won't this lamba compile
            string displayLabel = DisplayLabelFunction(timeZoneInfo) ?? (() => 
            {
                string positiveOrNegative = timeZoneInfo.BaseUtcOffset < TimeSpan.Zero ? "-" : "+";
                return $"GMT{positiveOrNegative}{timeZoneInfo.BaseUtcOffset:hh\\:mm}";
            });
            */

            //if the user specified a function to get the tooltip use that

            //Why do these not work?
            //var tooltip = ToolTipFunction?.Invoke(timeZoneInfo) ?? timeZoneInfo.StandardName;
            //var tooltip = ToolTipFunction(timeZoneInfo) ?? timeZoneInfo.StandardName;
            string tooltip;
            if (ToolTipFunction is null)
            {
                tooltip = timeZoneInfo.StandardName;
            }
            else
            {
                tooltip = ToolTipFunction(timeZoneInfo);
            }

            return new TUITimeZone
            {
                TimezoneName = timezoneName,
                DisplayLabel = displayLabel,
                Tooltip = tooltip
            };
        }
    }
}
