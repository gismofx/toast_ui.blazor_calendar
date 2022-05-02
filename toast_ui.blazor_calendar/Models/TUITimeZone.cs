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
        public string timezoneName { get; set; }
        /// <summary>
        /// Time Zone Display Label
        /// </summary>
        public string displayLabel { get; set; }
        /// <summary>
        /// Time Zone Tool Tip
        /// </summary>
        public string tooltip { get; set; }
    }
}
