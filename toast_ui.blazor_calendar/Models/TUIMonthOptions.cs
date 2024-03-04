using System.Globalization;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using toast_ui.blazor_calendar.Models.Theme;
using toast_ui.blazor_calendar.Services;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/MonthOptions
    /// </summary>


    public class TUIMonthOptions
    {
        /// <summary>
        /// The day names in monthly. Default values are 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[] DayNames { get; set; } = null;

        /// <summary>
        /// Start day of the week. 
        /// Available values are 0 (Sunday) to 6 (Saturday).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? StartDayOfWeek { get; set; } = 0;

        /// <summary>
        /// Make weekend column narrow(1/2 width)
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? NnarrowWeekend { get; set; } = false;

        /// <summary>
        /// Number of weeks to display. 
        /// 0 means display all weeks.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? VisibleWeeksCount { get; set; } = 0;

        /// <summary>
        /// Whether to exclude Saturday and Sunday.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Workweek { get; set; } = false;

        /// <summary>
        /// Specifies the maximum number of events displayed for each date in the monthly view. The default is 6.
        /// Even though you set this option, if the height of the date is insufficient, the option is automatically ignored.
        /// It is affected by the entire calendar area and the gridCell property of the month theme.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? VisibleEventCount { get; set; } = 6;
    }
}
