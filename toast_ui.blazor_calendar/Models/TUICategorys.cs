using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Define the Category's of an Event
    /// </summary>
    public static class TUICategorys
    {
        /// <summary>
        /// Event is a Milestone
        /// </summary>
        public static string Milestone = "milestone";
        /// <summary>
        /// Event is a Task
        /// </summary>
        public static string Task = "task";
        /// <summary>
        /// Event is a Allday
        /// </summary>
        public static string Allday = "allday";
        /// <summary>
        /// Event is time defined
        /// </summary>
        public static string Time = "time";
    }
}
