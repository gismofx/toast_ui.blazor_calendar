namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/WeekOptions
    /// </summary>
    public class TUIWeekOptions
    {
        /// <summary>
        /// The start day of week,
        /// </summary>
        public int startDayOfWeek { get; set; } = 0;

        /// <summary>
        /// The day names in weekly and daily.
        /// Default values are 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'
        /// </summary>
        public string[] daynames { get; set; }

        /// <summary>
        /// Make weekend column narrow(1/2 width)
        /// </summary>
        public bool narrowWeekend { get; set; } = false;

        /// <summary>
        /// Show only 5 days except for weekend
        /// </summary>
        public bool workweek { get; set; } = false;

        /// <summary>
        /// Show a collapse button to close multiple timezones
        /// </summary>
        public bool showTimezoneCollapseButton { get; set; } = false;

        /// <summary>
        /// An initial multiple timezones collapsed state
        /// </summary>
        public bool timezonesCollapsed { get; set; } = false;

        /// <summary>
        /// Can limit of render hour start.
        /// </summary>
        public int hourStart { get; set; } = 0;

        /// <summary>
        /// Can limit of render hour end.
        /// </summary>
        public int hourEnd { get; set; } = 24;
    }
}