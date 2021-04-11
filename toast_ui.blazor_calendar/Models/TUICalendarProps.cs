namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/CalendarProps
    /// </summary>
    public class TUICalendarProps
    {
        /// <summary>
        /// The calendar id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// The calendar name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The text color when schedule is displayed
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// The background color schedule is displayed
        /// </summary>
        public string bgColor { get; set; }

        /// <summary>
        /// The color of left border or bullet point when schedule is displayed
        /// </summary>
        public string borderColor { get; set; }

        /// <summary>
        /// The background color when schedule dragging
        /// </summary>
        public string dragBgColor { get; set; }
    }
}