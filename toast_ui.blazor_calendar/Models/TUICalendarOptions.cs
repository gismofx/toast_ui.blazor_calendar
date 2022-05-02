using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using toast_ui.blazor_calendar.Services;
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

        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            //# private method to compare members.
            return CompareMembers(obj as TUICalendarOptions);

        }

        private bool CompareMembers(TUICalendarOptions options)
        {
            if (!defaultView.Value.Equals(options.defaultView.Value))
            {
                return false;
            }
            if (!taskView.Equals(options.taskView))
            {
                return false;
            }
            if (!scheduleView.Equals(options.scheduleView))
            {
                return false;
            }
            if (!theme.Equals(options.theme))
            {
                return false;
            }
            if (!TUItemplate.Equals(options.TUItemplate))
            {
                return false;
            }
            if (!week.Equals(options.week))
            {
                return false;
            }
            if (!month.Equals(options.month))
            {
                return false;
            }
            if (!calendars.Equals(options.calendars))
            {
                return false;
            }
            if (!useCreationPopup.Equals(options.useCreationPopup))
            {
                return false;
            }
            if (!useDetailPopup.Equals(options.useDetailPopup))
            {
                return false;
            }
            /*
            if (!timezome.Equals(options.timezone))
            {
                return false;
            }
            */
            if (!disableDblClick.Equals(options.disableDblClick))
            {
                return false;
            }
            if (!disableClick.Equals(options.disableClick))
            {
                return false;
            }
            if (!isReadOnly.Equals(options.isReadOnly))
            {
                return false;
            }
            if (!usageStatistics.Equals(options.usageStatistics))
            {
                return false;
            }
            return true;
        }

    }

    public class TUICalendarViewName
    {
        private TUICalendarViewName(string value)
        {
            Value = value;
        }
        public string Value { get; set; }

        public static TUICalendarViewName Day { get { return new TUICalendarViewName("day"); } }
        public static TUICalendarViewName Week { get { return new TUICalendarViewName("week"); } }
        public static TUICalendarViewName Month { get { return new TUICalendarViewName("month"); } }

    }

public class TUITaskView
{
    private TUITaskView(string[] value)
    {
        Value = value;
    }
    public string[] Value { get; set; }

    public static TUITaskView MilestoneAndTask { get { return new TUITaskView(new[] { "milestone", "task" });  }}
    public static TUITaskView Milestone { get { return new TUITaskView(new[] { "milestone" }); } }
    public static TUITaskView Task { get { return new TUITaskView(new[] { "task" }); } }
    public static TUITaskView None { get { return new TUITaskView(new[] { "" }); } }
}
}