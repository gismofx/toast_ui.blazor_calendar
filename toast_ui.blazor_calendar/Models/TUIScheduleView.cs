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
    public class TUIScheduleView
    {
        public static TUIScheduleView AlldayAndTime { get { return new TUIScheduleView(new[] { "allday", "time" }); } } 
        public static TUIScheduleView Allday { get { return new TUIScheduleView(new[] { "allday" }); } }
        public static TUIScheduleView Time { get { return new TUIScheduleView(new[] { "time" }); } }
        public static TUIScheduleView None { get { return new TUIScheduleView(new[] { "" }); } }

        private TUIScheduleView(string[] value)
        {
            Value = value;
        }

        public static implicit operator TUIScheduleView(string[] value)
        {
            return new TUIScheduleView(value);
        }

        public static implicit operator TUIScheduleView(bool value)
        {
            if(value) return AlldayAndTime;
            return None;
        }
        public string[] Value { get; set; }

        
    }
}
