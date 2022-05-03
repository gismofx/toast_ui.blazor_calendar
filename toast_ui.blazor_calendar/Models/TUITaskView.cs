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
    public class TUITaskView
    {

        public static TUITaskView MilestoneAndTask { get { return new TUITaskView(new[] { "milestone", "task" }); } }
        public static TUITaskView Milestone { get { return new TUITaskView(new[] { "milestone" }); } }
        public static TUITaskView Task { get { return new TUITaskView(new[] { "task" }); } }
        public static TUITaskView None { get { return new TUITaskView(new[] { "" }); } }

        private TUITaskView(string[] value)
        {
            Value = value;
        }
        public string[] Value { get; set; }

    }
}
