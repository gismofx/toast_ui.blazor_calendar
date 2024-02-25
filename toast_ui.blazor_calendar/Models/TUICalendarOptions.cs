using System;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Used when instantiating a calendar.
    /// This is the main options class for a TUI calendar display and functions
    /// https://nhn.github.io/tui.calendar/latest/CalendarCore
    /// https://github.com/nhn/tui.calendar/blob/main/docs/en/apis/calendar.md
    /// </summary>
    public class TUICalendarOptions
    {
        /// <summary>
        /// Default view of calendar. The default value is 'week'.
        /// </summary>
        [JsonConverter(typeof(TUICalendarViewNameJsonConverter))]
        public TUICalendarViewName defaultView { get; set; }

        /// <summary>
        /// Whether use default creation popup or not. The default value is false.
        /// </summary>
        public bool useFormPopup { get; set; } = false;

        /// <summary>
        /// Whether use default detail popup or not. The default value is false.
        /// </summary>
        public bool useDetailPopup { get; set; } = false;

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

        /*
        //eventFilter = some js function. maybe can be a string that can be evaluated as a function
        */

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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TUICalendarProps[] calendars { get; set; } = null;

        /// <summary>
        /// Whether to enable grid selection. or it's option. 
        /// it's enabled when the value is an object and will be disabled when is true.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TUIGridSelection gridSelection { get; set; } = null;

        /// <summary>
        /// Timezone - Set a custom time zone.
        /// You can add secondary timezone in the weekly/daily view.
        /// https://nhn.github.io/tui.calendar/latest/Timezone
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TUICalendarTimeZoneOption timezone { get; set; } = null;

        /// <summary>
        /// themeConfig for custom style.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        /// 
        public TUITheme theme { get; set; } = null;

        /// <summary>
        /// https://nhn.github.io/tui.calendar/latest/Template
        /// had to rename from 'template' - violates naming rule
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("template")]
        public TUITemplate TUITemplate { get; set; } = null;


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
            //if (!taskView.Equals(other.taskView))
            //{
            //    return false;
            //}
            //if (!scheduleView.Equals(other.scheduleView))
            //{
            //    return false;
            //}
            if (!theme.Equals(other.theme))
            {
                return false;
            }
            if (!TUITemplate.Equals(other.TUITemplate))
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
            if (!useFormPopup.Equals(other.useFormPopup))
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
            //if (!disableDblClick.Equals(other.disableDblClick))
            //{
            //    return false;
            //}
            //if (!disableClick.Equals(other.disableClick))
            //{
            //    return false;
            //}
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
            hashCode.Add(theme);
            hashCode.Add(week);
            hashCode.Add(month);
            hashCode.Add(calendars);
            hashCode.Add(useFormPopup);
            hashCode.Add(useDetailPopup);
            hashCode.Add(timezone);
            hashCode.Add(isReadOnly);
            return hashCode.ToHashCode();
        }
    }
}
