using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using toast_ui.blazor_calendar.Services;
using toast_ui.blazor_calendar.Services.JsonConverters;

namespace toast_ui.blazor_calendar.Models
{
    /// <summary>
    /// https://nhn.github.io/tui.calendar/latest/MonthOptions
    /// </summary>


    public class TUIMonthOptions
    {
        /// <summary>
        /// Start day of the week. 
        /// Available values are 0 (Sunday) to 6 (Saturday).
        /// </summary>
        public int startDayOfWeek { get; set; } = 0;


        /// <summary>
        /// The day names in monthly. Default values are 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[] dayNames { get; set; } = null;

        /// <summary>
        /// Whether to exclude Saturday and Sunday.
        /// </summary>
        public bool workweek { get; set; } = false;


        /// <summary>
        /// Make weekend column narrow(1/2 width)
        /// </summary>
        public bool narrowWeekend { get; set; } = false;


        /// <summary>
        /// Number of weeks to display. 
        /// 0 means display all weeks.
        /// </summary>
        public int visibleWeeksCount { get; set; } = 0;


        /*
        public override bool Equals(object obj)
        {
            if (null == obj)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            //# private method to compare members.
            return CompareMembers(obj as TUIMonthOptions);

        }

        private bool CompareMembers(TUIMonthOptions options)
        {
            if (this.daynames.SequenceEqual(options.daynames))
            {
                
            }
        }
        */
    }
}
