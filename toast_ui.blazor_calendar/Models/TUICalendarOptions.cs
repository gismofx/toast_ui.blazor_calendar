using System;

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
        public string defaultView { get; set; }

        /// <summary>
        /// Show the milestone and task in weekly, daily view.
        /// The default value is true. If the value is array, it can be ['milestone', 'task'].
        /// </summary>
        public string taskView { get; set; } = "true";

        /// <summary>
        /// Show the all day and time grid in weekly, daily view.
        /// The default value is false.
        /// If the value is array, it can be ['allday', 'time'].
        /// </summary>
        public string scheduleView { get; set; } = "true";

        /// <summary>
        /// themeConfig for custom style.
        /// </summary>
        public TUIThemeConfig theme { get; set; } = null;

        /// <summary>
        /// https://nhn.github.io/tui.calendar/latest/Template
        /// had to rename from 'template' - violates naming rule
        /// </summary>
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
        public string[] calendars { get; set; }

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
        public TimeZoneInfo timezone { get; set; } //Todo: Map this or implement TUI timezone class

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
        public bool usageStatistics = true;
    }
}