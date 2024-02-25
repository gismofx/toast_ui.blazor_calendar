using System.Text.Json.Serialization;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/WeekOptions
    /// </summary>
    public class TUIWeekOptions
    {
        /// <summary>
        /// Start day of the week.
        /// Available values are 0 (Sunday) to 6 (Saturday).
        /// </summary>
        public int startDayOfWeek { get; set; } = 0;

        /// <summary>
        /// The day names in weekly and daily.
        /// Default value is null. Then, default day names are 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[] dayNames { get; set; } = null;


        /// <summary>
        /// Whether to exclude Saturday and Sunday.
        /// </summary>
        public bool workweek { get; set; } = false;

        /// <summary>
        /// Show a collapse button to close multiple timezones
        /// </summary>
        public bool showTimezoneCollapseButton { get; set; } = true;


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

        /// <summary>
        /// Make weekend column narrow(1/2 width)
        /// </summary>
        public bool narrowWeekend { get; set; } = false;

        /// <summary>
        /// Determine which view to display events. 
        /// Available values are 'allday' and 'time'. set to true disable event view.
        /// </summary>
        public bool eventView { get; set; } = true;


        /// <summary>
        /// Show the milestone and task in weekly, daily view.
        /// The default value is true. If the value is array, it can be ['milestone', 'task'].
        /// </summary>
        //[JsonConverter(typeof(TUITaskViewJsonConverter))]
        public bool taskView { get; set; } = true;

        //Whether to collapse duplicate events. If you want to filter duplicate events and choose the main event based on your requirements, set and . For more information, see Options in guide.
        //collapseDuplicateEvents
    }
}
