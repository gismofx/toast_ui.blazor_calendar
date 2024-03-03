namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Class for holding time zone information as prescribed at https://nhn.github.io/tui.calendar/latest/Timezone
    /// </summary>
    public class TUITimeZone
    {
        /// <summary>
        /// Time Zone Name
        /// </summary>
        public string TimezoneName { get; set; }
        /// <summary>
        /// Time Zone Display Label
        /// </summary>
        public string DisplayLabel { get; set; }
        /// <summary>
        /// Time Zone Tool Tip
        /// </summary>
        public string Tooltip { get; set; }
    }
}
