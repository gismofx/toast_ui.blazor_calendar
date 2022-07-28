using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Enum Class/Helper For TUI Schedule View Modes
    /// </summary>
    public static class TUIScheduleView
    {
        public static string[] AlldayAndTime = new[] { "allday", "time" }; 
        public static string[] Allday = new[] { "allday" };
        public static string[] Time = new[] { "time" };
        public static string[] None = new[] { "" };

    }
}
