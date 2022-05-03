using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Enum Class/Helper For TUI Calendar Views
    /// </summary>
    public class TUICalendarViewName
    {
        public static TUICalendarViewName Day { get { return new TUICalendarViewName("day"); } }
        public static TUICalendarViewName Week { get { return new TUICalendarViewName("week"); } }
        public static TUICalendarViewName Month { get { return new TUICalendarViewName("month"); } }

        private TUICalendarViewName(string value)
        {
            Value = value;
        }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

    }
}
