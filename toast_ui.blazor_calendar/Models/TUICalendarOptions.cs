using System;
using System.Text.Json.Serialization;

namespace toast_ui.blazor_calendar.Models
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum TUICalendarViewName
    {
        [JsonPropertyName("day")] Day,
        [JsonPropertyName("week")] Week,
        [JsonPropertyName("month")] Month
    }

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
        public TUICalendarViewName DefaultView { get; set; }

        /// <summary>
        /// Whether use default creation popup or not. The default value is false.
        /// </summary>
        public bool UseFormPopup { get; set; } = false;

        /// <summary>
        /// Whether use default detail popup or not. The default value is false.
        /// </summary>
        public bool UseDetailPopup { get; set; } = false;

        /// <summary>
        /// Calendar is read-only mode and a user can't create and modify any schedule.
        /// The default value is false.
        /// </summary>
        public bool IsReadOnly { get; set; } = false;

        /// <summary>
        /// Let us know the hostname.
        /// If you don't want to send the hostname, please set to false.
        /// </summary>
        public bool UsageStatistics { get; set; } = true;

        /*
        //eventFilter = some js function. maybe can be a string that can be evaluated as a function
        */

        /// <summary>
        /// Week options for view
        /// </summary>
        public TUIWeekOptions Week { get; set; } = null;

        /// <summary>
        /// Month options for view
        /// </summary>
        public TUIMonthOptions Month { get; set; } = null;

        /// <summary>
        /// CalendarProps List that can be used to add new schedule. The default value is [].
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CalendarInfo[] Calendars { get; set; } = null;

        /// <summary>
        /// Whether to enable grid selection. or it's option. 
        /// it's enabled when the value is an object and will be disabled when is true.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TUIGridSelection GridSelection { get; set; } = null;

        /// <summary>
        /// Timezone - Set a custom time zone.
        /// You can add secondary timezone in the weekly/daily view.
        /// https://nhn.github.io/tui.calendar/latest/Timezone
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TUICalendarTimeZoneOption Timezone { get; set; } = null;

        /// <summary>
        /// themeConfig for custom style.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        /// 
        public TUITheme Theme { get; set; } = null;

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
            if (!DefaultView.Equals(other.DefaultView))
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
            if (!Theme.Equals(other.Theme))
            {
                return false;
            }
            if (!TUITemplate.Equals(other.TUITemplate))
            {
                return false;
            }
            if (!Week.Equals(other.Week))
            {
                return false;
            }
            if (!Month.Equals(other.Month))
            {
                return false;
            }
            if (!Calendars.Equals(other.Calendars))
            {
                return false;
            }
            if (!UseFormPopup.Equals(other.UseFormPopup))
            {
                return false;
            }
            if (!UseDetailPopup.Equals(other.UseDetailPopup))
            {
                return false;
            }
            if (!Timezone.Equals(other.Timezone))
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
            if (!IsReadOnly.Equals(other.IsReadOnly))
            {
                return false;
            }
            if (!UsageStatistics.Equals(other.UsageStatistics))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(DefaultView);
            hashCode.Add(Theme);
            hashCode.Add(Week);
            hashCode.Add(Month);
            hashCode.Add(Calendars);
            hashCode.Add(UseFormPopup);
            hashCode.Add(UseDetailPopup);
            hashCode.Add(Timezone);
            hashCode.Add(IsReadOnly);
            return hashCode.ToHashCode();
        }
    }
}
