using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// Enum Class/Helper For TUI Task View Modes
    /// </summary>
    public static class TUITaskView
    {
        public static string[] MilestoneAndTask = new[] { "milestone", "task" };
        public static string[] Milestone = new[] { "milestone" };
        public static string[] Task = new[] { "task" };
        public static string[] None = new[] { "" };
    }
}
