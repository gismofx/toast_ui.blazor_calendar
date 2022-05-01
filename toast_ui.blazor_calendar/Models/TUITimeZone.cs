using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/Zone
    /// </summary>
    public class TUITimeZone
    {
        /// <summary>
        /// timezone name (time zone names of the IANA time zone database, such as 'Asia/Seoul', 'America/New_York').
        /// Basically, it will calculate the offset using 'Intl.DateTimeFormat' with the value of the this property entered.
        /// This property is required.
        /// </summary>
        public string timezoneName { get; set; }
        /// <summary>
        /// The display label of your timezone at weekly/daily view(e.g. 'GMT+09:00')
        /// </summary>
        public string displayLabel { get; set; }
        /// <summary>
        /// The tooltip(e.g. 'Seoul')
        /// </summary>
        public string tooltip { get; set; }
        /// <summary>
        /// The minutes for your timezone offset. If null, use the browser's timezone. Refer to Date.prototype.getTimezoneOffset().
        /// This property will be deprecated. (since version 1.13)
        /// </summary>
        public double timezoneOffset { get; set; }
    }
}
