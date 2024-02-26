using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// States of an Event
    /// </summary>
    public static class TUIStates
    {
        /// <summary>
        /// Status is Busy
        /// </summary>
        public static string Busy = "Busy";
        /// <summary>
        /// Status is Free
        /// </summary>
        public static string Free = "Free";
    }
}
