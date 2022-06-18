using System;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Used when instantiating a calendar.
    /// This is the main options class for a TUI calendar display and functions
    /// https://nhn.github.io/tui.calendar/latest/Options
    /// </summary>
    public class TUICalendarOptions
    {
        /// <summary>
        /// Default view of calendar. The default value is 'week'.
        /// </summary>
        [JsonConverter(typeof(TUICalendarViewNameJsonConverter))]
        public TUICalendarViewName defaultView { get; set; }

        /// <summary>
        /// Show the milestone and task in weekly, daily view.
        /// The default value is true. If the value is array, it can be ['milestone', 'task'].
        /// </summary>
        //[JsonConverter(typeof(TUITaskViewJsonConverter))]
        public bool taskView { get; set; } = true;

        /// <summary>
        /// Show the all day and time grid in weekly, daily view.
        /// The default value is false.
        /// If the value is array, it can be ['allday', 'time'].
        /// </summary>
        //public string[] scheduleView { get; set; } = new[] { "allday", "time" };
        public bool scheduleView { get; set; } = true; 

        /// <summary>
        /// themeConfig for custom style.
        /// </summary>
        public TUIThemeConfig theme { get; set; } = null;

        /// <summary>
        /// https://nhn.github.io/tui.calendar/latest/Template
        /// had to rename from 'template' - violates naming rule
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("template")]
        public TUITemplate TUItemplate { get; set; } = null;

        /// <summary>
        /// Week options for view
        /// </summary>
        public TUIWeekOptions week { get; set; } = null;

        /// <summary>
        /// Month options for view
        /// </summary>
        public TUIMonthOptions month { get; set; } = null;

        /// <summary>
        /// CalendarProps List that can be used to add new schedule. The default value is [].
        /// </summary>
        public TUICalendarProps[] calendars { get; set; }

        /// <summary>
        /// Whether use default creation popup or not. The default value is false.
        /// </summary>
        public bool useCreationPopup { get; set; } = false;

        /// <summary>
        /// Whether use default detail popup or not. The default value is false.
        /// </summary>
        public bool useDetailPopup { get; set; } = false;

        /// <summary>
        /// Timezone - Set a custom time zone.
        /// You can add secondary timezone in the weekly/daily view.
        /// https://nhn.github.io/tui.calendar/latest/Timezone
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TUICalendarTimeZoneOption timezone { get; set; } = null;

        /// <summary>
        /// Disable double click to create a schedule.
        /// The default value is false.
        /// </summary>
        public bool disableDblClick { get; set; } = false;

        /// <summary>
        /// Disable click to create a schedule. The default value is false.
        /// </summary>
        public bool disableClick { get; set; } = false;

        /// <summary>
        /// Calendar is read-only mode and a user can't create and modify any schedule.
        /// The default value is false.
        /// </summary>
        public bool isReadOnly { get; set; } = false;

        /// <summary>
        /// Let us know the hostname.
        /// If you don't want to send the hostname, please set to false.
        /// </summary>
        public bool usageStatistics { get; set; } = true;

        /// <summary>
        /// This method is override. Ultimately, compares each property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            
            return CompareMembers((TUICalendarOptions)obj);
        }

        private bool CompareMembers(TUICalendarOptions other)
        {
            if (!defaultView.Value.Equals(other.defaultView.Value))
            {
                return false;
            }
            if (!taskView.Equals(other.taskView))
            {
                return false;
            }
            if (!scheduleView.Equals(other.scheduleView))
            {
                return false;
            }
            if (!theme.Equals(other.theme))
            {
                return false;
            }
            if (!TUItemplate.Equals(other.TUItemplate))
            {
                return false;
            }
            if (!week.Equals(other.week))
            {
                return false;
            }
            if (!month.Equals(other.month))
            {
                return false;
            }
            if (!calendars.Equals(other.calendars))
            {
                return false;
            }
            if (!useCreationPopup.Equals(other.useCreationPopup))
            {
                return false;
            }
            if (!useDetailPopup.Equals(other.useDetailPopup))
            {
                return false;
            }
            if (!timezone.Equals(other.timezone))
            {
                return false;
            }
            if (!disableDblClick.Equals(other.disableDblClick))
            {
                return false;
            }
            if (!disableClick.Equals(other.disableClick))
            {
                return false;
            }
            if (!isReadOnly.Equals(other.isReadOnly))
            {
                return false;
            }
            if (!usageStatistics.Equals(other.usageStatistics))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(defaultView);
            hashCode.Add(taskView);
            hashCode.Add(scheduleView);
            hashCode.Add(theme);
            hashCode.Add(week);
            hashCode.Add(month);
            hashCode.Add(calendars);
            hashCode.Add(useCreationPopup);
            hashCode.Add(useDetailPopup);
            hashCode.Add(timezone);
            hashCode.Add(isReadOnly);
            return hashCode.ToHashCode();
        }
    }
}
