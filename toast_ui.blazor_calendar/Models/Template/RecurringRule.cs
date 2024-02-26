using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace toast_ui.blazor_calendar.Models.Template
{
    /// <summary>
    /// recurrenceRule: 'FREQ=WEEKLY;UNTIL=20241231'
    /// </summary>
    public class RecurringRule
    {
        public RecurringRule()
        {
            
        }
    }

    public interface IRecurringRule
    {
        /// <summary>
        /// Defines the Frequency of the Recurrence
        /// </summary>
        public RecurrenceTypes Frequency { get; set; }

        /// <summary>
        /// Defines the End of the Recurrence
        /// </summary>
        public DateTime? Until { get; set; }
    }

    public enum RecurrenceTypes
    {
        [JsonPropertyName("daily")]
        Daily,
        [JsonPropertyName("weekly")]
        Weekly,
        [JsonPropertyName("monthly")]
        Monthly,
        [JsonPropertyName("yearly")]
        Yearly
    }
}
